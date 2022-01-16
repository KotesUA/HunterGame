// CameraController.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConcoller : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    private float _z;
    private void Start()
    {
        _z = transform.position.z;
    }
    void Update()
    {        
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, _z); // Camera follows the player with specified offset position
    }
}
