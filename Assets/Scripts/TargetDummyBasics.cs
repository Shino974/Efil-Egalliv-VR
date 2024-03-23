using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummyBasics : MonoBehaviour
{
    [SerializeField] private Animator dummyAnimator;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Sword") || other.gameObject.CompareTag("Bullet"))
        {
            dummyAnimator.SetTrigger("Death");
        }
    }

    public void ActivateDummy()
    {
        dummyAnimator.SetTrigger("Activate");
    }
}
