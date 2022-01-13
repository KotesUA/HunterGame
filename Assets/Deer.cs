using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deer : MonoBehaviour
{
    [SerializeField]
    private float Speed;

    [SerializeField]
    private float LookRadius = 20;

    private Vector3 wayPoint;
    private Vector3 target;

    private AnimalState state;

    private void
        Deers_ChangeTargetEvent(Vector3 newTarget)
    {
        wayPoint = newTarget;
    }
    public void SetGroup(GroupOfDeers deers)
    {
        deers.ChangeTargetEvent += Deers_ChangeTargetEvent;
    }
        
    void Update()
    {
        SetState();
        switch(state)
        {
            case AnimalState.Run:
                transform.position -= Speed * 2 * Time.deltaTime * (target - transform.position).normalized;
                break;
            case AnimalState.Walk:
                transform.position = Vector2.MoveTowards(transform.position, wayPoint, Speed * Time.deltaTime);
                break;
        }
    }

    private void SetState()
    {
        Collider2D[] coliders = Physics2D.OverlapCircleAll(transform.position, LookRadius).Where(x => x.gameObject.tag == "Player" || x.gameObject.tag == "Wolf").ToArray();
        if (coliders.Length == 0)
        {
            state = AnimalState.Walk;
        }
        else 
        { 
            SetTarget(coliders);
        }
    }

    private void SetTarget(Collider2D[] coliders)
    {
        if (state == AnimalState.Walk)
        {
            target = coliders.OrderBy(x => Vector2.Distance(x.transform.position, transform.position)).First().transform.position;
            state = AnimalState.Run;
        }
        if (target == Vector3.zero || Vector2.Distance(transform.position, target) < 0.2)
        {
            target = coliders.OrderBy(x => Vector2.Distance(x.transform.position, transform.position)).First().transform.position;
            state = AnimalState.Run;
        }
    }
}
