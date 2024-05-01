using System;
using UnityEngine;
using UnityEngine.Rendering;

public class DummyTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] targetDummies;
    public int dummyCount;

    private void Awake()
    {
        dummyCount = targetDummies.Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject dummy in targetDummies)
            {
                dummy.GetComponent<TargetDummyBasics>().ActivateDummy();
            }
        }
    }

    public void CountdownDummy()
    {
        dummyCount--;
    }
}
