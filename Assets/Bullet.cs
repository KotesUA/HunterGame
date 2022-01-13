using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private Vector3 _direction;

    void Update()
    {
        transform.position += _speed * Time.deltaTime * _direction;
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Hunter") || collider.gameObject.CompareTag("Bound"))
        {
            return;
        }
        Destroy(collider.gameObject);
        Destroy(gameObject);
    }
}
