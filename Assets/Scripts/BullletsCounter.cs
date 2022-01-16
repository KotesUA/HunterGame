//BulletsCounter.cs

using UnityEngine;
using UnityEngine.UI;

public class BullletsCounter : MonoBehaviour
{
    private Text _bulletCount;

    private void Awake()
    {
        _bulletCount = GetComponent<Text>();
    }
   
    public void ChangeBulletCount(int count)
    { 
        _bulletCount.text = "Bullets: " + count.ToString();       
    }    
}
