using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("Bound"))
        {
            Destroy(collision.gameObject);
        }
    }
}
// любой обект который не граница при соприкосновении с границей уничтожается