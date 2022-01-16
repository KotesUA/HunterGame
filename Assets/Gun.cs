// Gun.cs

using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IGun
{
    [SerializeField]
    private BullletsCounter _bullletsCounter;

    [SerializeField]
    private int _bulletCount;

    Vector3 aimDirection;
    public Bullet bullet;
    private void Start()
    {// к буллет каунтер вызывает функцию изменения количества пуль
        _bullletsCounter.ChangeBulletCount(_bulletCount);
    }

    private void Update()
    {
        //Знаходим кут між мишкою і напрямком пістолету
        Vector3 mousePosition = GetMouseWorldPosition();       
         aimDirection = (mousePosition - transform.position).normalized;
        //перевод из полярной системы координат в декартовая
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;   
        //Повертаєм на потрібний кут
         transform.localEulerAngles = new Vector3(0, 0, angle);        
    }

    //Get Mouse Position in the World with Z = 0f
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
  
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;//получаем позицию миши в мире
    }

    public void Shoot()
    {
        if (_bulletCount == 0)
            return;
        //создаем новую пулю 
        Bullet bulletClone = Instantiate(bullet, transform.position, transform.rotation);
        bulletClone.SetDirection(aimDirection);//устанавливаем направление пули в ту сторону в которую целимся
         _bulletCount--;
        _bullletsCounter.ChangeBulletCount(_bulletCount);//отняли и обновили булеткаунтер
    }
}