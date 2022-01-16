//Rabbit.cs

using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rabbit : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        SetState();
    }


    void Update()
    {
       
        SetState();
        switch (state)
        {
            case AnimalState.Run://если состояние убегать то убегаем с заданой в состоянии скоростью(двойной) от таргета
                transform.position -= Speed * Time.deltaTime * (target - transform.position).normalized;
                break;
            case AnimalState.Walk://ходит с заданой скоростью
                transform.position += Speed / 2 * Time.deltaTime * (wayPoint - transform.position).normalized;
                break;
        }
    }
    private void SetState()
    {//ищем столкновения с игроком или волком
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, LookRadius);
        if (colliders.Length <= 1)//если столкновений нет идем
            SetNewDestination();
        else
            SetTarget(colliders);
    }

    private void SetTarget(Collider2D[] colliders)
    {
        if (state == AnimalState.Walk)
        {
            target = colliders.Where(x => x.gameObject.GetInstanceID() != gameObject.GetInstanceID())
                .OrderBy(x => Vector2.Distance(x.transform.position, transform.position)).First().transform.position;
            state = AnimalState.Run;
        }
        if (target == Vector3.zero || Vector2.Distance(transform.position, target) < 0.2)
        {
            target = colliders.Where(x => x.gameObject.GetInstanceID() != gameObject.GetInstanceID())
                .OrderBy(x => Vector2.Distance(x.transform.position, transform.position)).First().transform.position;
            state = AnimalState.Run;
        }
    }

    private void SetNewDestination() //добавляем это из за того что нет группы у зайца
    {
        if (state == AnimalState.Run)
        {//задаем рэнж по иксу и по игрику
            wayPoint = new Vector2(Random.Range(-_maxDistance, _maxDistance), Random.Range(-_maxDistance, _maxDistance));
            state = AnimalState.Walk;
            return;
        }//если нет цели ставим новую
        if (wayPoint == Vector3.zero)
        {
            wayPoint = new Vector2(Random.Range(-_maxDistance, _maxDistance), Random.Range(-_maxDistance, _maxDistance));
            state = AnimalState.Walk;
            return;
        }// если меньше трех пикселей до цели то ставим новую цель
        if (Vector2.Distance(wayPoint, transform.position) < 3)
        {
            wayPoint = new Vector2(Random.Range(-_maxDistance, _maxDistance), Random.Range(-_maxDistance, _maxDistance));
            state = AnimalState.Walk;
            return;
        }
    }
}
