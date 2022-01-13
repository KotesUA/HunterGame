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
    private int _MaxDistance;
    private AnimalState state;


    private Vector3 WayPoint;
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
                transform.position += Speed / 2 * Time.deltaTime * (WayPoint - transform.position).normalized;
                break;
        }


    }
    private void SetState()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, LookRadius);
        if (colliders.Length <= 1)
        {
            SetNewDestination();
        }
        else
        {
            SetTarget(colliders);
        }

    }
    private void SetTarget(Collider2D[] coliders)
    {
        if (state == AnimalState.Walk)
        {
            target = coliders.Where(x => x.gameObject.GetInstanceID() != gameObject.GetInstanceID()).OrderBy(x => Vector2.Distance(x.transform.position, transform.position)).First().transform.position;
            state = AnimalState.Run;
        }
        if (target == Vector3.zero || Vector2.Distance(transform.position, target) < 0.2)
        {
            target = coliders.Where(x => x.gameObject.GetInstanceID() != gameObject.GetInstanceID()).OrderBy(x => Vector2.Distance(x.transform.position, transform.position)).First().transform.position;
            state = AnimalState.Run;
        }
    }
    private void SetNewDestination()
    {
        if (state == AnimalState.Run)
        {
            WayPoint = new Vector2(Random.Range(-_MaxDistance, _MaxDistance),
                Random.Range(-_MaxDistance, _MaxDistance));
            state = AnimalState.Walk;
            return;
        }
        if (WayPoint == Vector3.zero)
        {
            WayPoint = new Vector2(Random.Range(-_MaxDistance, _MaxDistance),
                Random.Range(-_MaxDistance, _MaxDistance));
            state = AnimalState.Walk;
            return;
        }
        if (Vector2.Distance(WayPoint, transform.position) < 3)
        {
            WayPoint = new Vector2(Random.Range(-_MaxDistance, _MaxDistance),
               Random.Range(-_MaxDistance, _MaxDistance));
            state = AnimalState.Walk;
            return;
        }
    }
}
