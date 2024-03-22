using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummyBasics : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("SMASH THAT ASS");
        }
    }
}
