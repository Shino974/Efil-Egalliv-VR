using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiBasics : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
