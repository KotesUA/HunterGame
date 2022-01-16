//Bullet.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    private Vector3 _direction; //встроенный тип
    
    void Update()
    {
        transform.position += _speed * Time.deltaTime *_direction;
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bound") || collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        Destroy(collision.gameObject);
        Destroy(gameObject);
    } 
}
