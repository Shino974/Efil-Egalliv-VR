using System.Collections;
using UnityEngine;

public class ZombiGeneratorScript : MonoBehaviour
{
    [Tooltip("The zombi prefab")]
    [SerializeField] private GameObject zombiPrefab;

    [Tooltip("The radius of the circle where the zombis will be generated")]
    [SerializeField] private float radius;

    private void Start()
    {
        StartCoroutine(GenerateZombis());
    }

    private IEnumerator GenerateZombis()
    {
        for (int i = 0; i < 30; i++) // 30 times for 1 minute (2 seconds interval)
        {
            int zombiCount = (i < 15) ? 1 : 2; // Generate 1 zombi for the first 30 seconds, then 2 zombis

            for (int j = 0; j < zombiCount; j++)
            {
                Vector3 randomPosition = Random.insideUnitSphere * radius;
                randomPosition += transform.position; // Add the generator's position
                randomPosition.y = 0;
                Instantiate(zombiPrefab, randomPosition, Quaternion.identity);
            }

            yield return new WaitForSeconds(2f); // Wait for 2 seconds
        }
    }
}