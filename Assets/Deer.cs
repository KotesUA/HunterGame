//Deer.cs

using Assets.Scripts.Enums;
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

    //Точкаа до якої рухається олень 
    private Vector3 wayPoint;

    private AnimalState state;//интерфейс где хранятся действия оленя
    
    private void Deers_ChangeTargetEvent(Vector3 newTarget)
    {
        // Задаємо нову точку
        wayPoint = newTarget;        
    }
    public void SetGroupe(GroupOfDeers deers)
    {
        //При зміні точки шляху 
        deers.ChangeTargetEvent += Deers_ChangeTargetEvent;//если у группы меняется цель и у конкретного оленя меняется цель
    }
   
    private Vector3 target;

   
    void Update()//вызывается при обновление оленя
    {
        SetState();
        switch (state)
        {
            case AnimalState.Run:
                //если состояние убегать то убегаем с заданой в состоянии скоростью(двойной) от таргета
                transform.position -= Speed*2 * Time.deltaTime * (target - transform.position).normalized;// normalize вовращает направление вектора 
                break;
            case AnimalState.Walk:
                //ходит с заданой скоростью
                transform.position = Vector2.MoveTowards(transform.position, wayPoint, Speed * Time.deltaTime);
                break;
        }
    }
   
    private void SetState()
    {   //ищем столкновения с игроком или волком
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, LookRadius)
            .Where(x => x.gameObject.tag == "Player" || x.gameObject.tag == "Wolf").ToArray();
        if (colliders.Length == 0)//если столкновений нет идем
            state = AnimalState.Walk;
        else//иначе бежим
            SetTarget(colliders);
    }

    private void SetTarget(Collider2D[] colliders)
    {
        if (state == AnimalState.Walk)
        {
            target = colliders
                .OrderBy(x => Vector2.Distance(x.transform.position, transform.position)).First().transform.position;
            state = AnimalState.Run;//берем первого врага по дистанции и бежим
        }
        //если уже бежим то сравниваем и выбираем нового врага
        if (target == Vector3.zero || Vector2.Distance(transform.position, target) < 0.2) 
        {
            target = colliders
              .OrderBy(x => Vector2.Distance(x.transform.position, transform.position)).First().transform.position;
            state = AnimalState.Run;
        }
    }

}
