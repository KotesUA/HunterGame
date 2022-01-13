using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCounter : MonoBehaviour
{
    private Text _bulletCount;

    private void Awake()
    {
        _bulletCount = GetComponent<Text>();
    }

    public void ChangeBulletCounter(int bullets)
    {
        _bulletCount.text = bullets.ToString();
    }
}
