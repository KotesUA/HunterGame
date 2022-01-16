using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterController : MonoBehaviour
{
    [SerializeField]
    [InspectorName("Hunter")]
    private Hunter _hunter;

    private void Update()
    {
        if (_hunter != null)
        {
            if (Input.GetKey(KeyCode.D))
            {
                _hunter.MoveRight();
            }
            if (Input.GetKey(KeyCode.A))
            {
                _hunter.MoveLeft();
            }
            if (Input.GetKey(KeyCode.W))
            {
                _hunter.MoveUp();
            }
            if (Input.GetKey(KeyCode.S))
            {
                _hunter.MoveDown();
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                _hunter.Shoot();
            }

        }
    }
}
