// Wolf.cs

using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    [SerializeField]
    private float Speed;

    [SerializeField]
    private float LookRadius = 20;
  
    [SerializeField]
    private int _maxDistance;
    private AnimalState state;

    private Vector3 wayPoint;
    private Vector3 target;

    
    void Start()
    {
        SetState();
    }

  
    void Update()
    {
        SetState();
        switch (state)
        {
            case AnimalState.Run:
                transform.position += Speed * Time.deltaTime * (target - transform.position).normalized;
                break;
            case AnimalState.Walk:
                transform.position += Speed/2 * Time.deltaTime * (wayPoint - transform.position).normalized;
                break;
        }     
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {//если волк сталкивается с границей то ничего
        if(collision.gameObject.CompareTag("Bound"))
        {
            return;
        }
        // если не с границей то убить объект столкновения
        Destroy(collision.gameObject);
        SetState();
    }

    private void SetState()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, LookRadius)
            .Where(x => x.CompareTag("Wolf") == false && x.CompareTag("Bound") == false).ToArray();
        if (colliders.Length == 0)
            SetNewDestination();
        else
            SetTarget(colliders);
    }

    private void SetTarget(Collider2D[] colliders)
    {
        if (state == AnimalState.Walk)
        {
            target = colliders.OrderBy(x => Vector2.Distance(x.transform.position, transform.position)).First().transform.position;
            state = AnimalState.Run;
        }
        if (target == Vector3.zero || Vector2.Distance(transform.position, target) < 0.2)
        {
            target = colliders.OrderBy(x => Vector2.Distance(x.transform.position, transform.position)).First().transform.position;
            state = AnimalState.Run;
        }
    }

    private void SetNewDestination()
    {
        if(state == AnimalState.Run)
        {
            wayPoint = new Vector2(Random.Range(-_maxDistance, _maxDistance), Random.Range(-_maxDistance, _maxDistance));
            state = AnimalState.Walk;
            return;
        }
        if (wayPoint == Vector3.zero)
        {
            wayPoint = new Vector2(Random.Range(-_maxDistance, _maxDistance), Random.Range(-_maxDistance, _maxDistance));
            state = AnimalState.Walk;
            return;
        }
        if (Vector2.Distance(wayPoint, transform.position) < 3)
        {

            wayPoint = new Vector2(Random.Range(-_maxDistance, _maxDistance), Random.Range(-_maxDistance, _maxDistance));
            state = AnimalState.Walk;
            return;
        }
    }
}
