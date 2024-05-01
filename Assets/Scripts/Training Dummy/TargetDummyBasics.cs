using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetDummyBasics : MonoBehaviour
{
    [SerializeField] private Animator dummyAnimator;
    [SerializeField] private DummyTrigger dummyTrigger;
    private ScoreDummy scoreDummy;
    public bool isDead = false;
    
    private void Awake()
    {
        scoreDummy = FindObjectOfType<ScoreDummy>();
        dummyTrigger = FindObjectOfType<DummyTrigger>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Sword") || other.gameObject.CompareTag("Bullet"))
        {
            dummyAnimator.SetTrigger("Death");
            isDead = true;
            scoreDummy.AddScore();
            this.GetComponent<BoxCollider>().enabled = false;
            dummyTrigger.CountdownDummy();
        }
    }

    public void ActivateDummy()
    {
        scoreDummy.StartDummyGame();
        dummyAnimator.SetTrigger("Activate");        
        dummyAnimator.ResetTrigger("Death");
    }
    
    public void DesactivateDummy()
    {   
        scoreDummy.ResetScore();
        dummyAnimator.ResetTrigger("Activate");
        dummyAnimator.SetTrigger("Death");
    }
}
