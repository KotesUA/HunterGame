using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : MonoBehaviour, IGun
{
    [SerializeField]
    private BulletCounter bulletCounter;

    [SerializeField]
    private int _bulletsCount;

    Vector3 aimDirection;
    public Bullet bullet;

    public void Shoot()
    {
        if(_bulletsCount == 0)
        {
            return;
        }
        Bullet bulletClone = Instantiate(bullet, transform.position, transform.rotation);
        bulletClone.SetDirection(aimDirection);
        _bulletsCount--;
        bulletCounter.ChangeBulletCounter(_bulletsCount);
    }

    void Start()
    {
        bulletCounter.ChangeBulletCounter(_bulletsCount);
    }

    void Update()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        aimDirection = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.localEulerAngles = new Vector3(0, 0, angle);
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0f;
        return worldPosition;
    }
}
