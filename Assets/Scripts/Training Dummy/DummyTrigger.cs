using UnityEngine;

public class DummyTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] targetDummies;

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
}
