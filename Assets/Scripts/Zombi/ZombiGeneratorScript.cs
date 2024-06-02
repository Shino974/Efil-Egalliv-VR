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
        yield return new WaitForSeconds(10f); // Wait for 10 seconds before starting

        int zombiCount = 1; // Start with 1 zombi

        for (int i = 0; i < 150; i++) // 150 times for 5 minutes (2 seconds interval)
        {
            if (i % 30 == 0 && i > 0) // Every minute (30 iterations), double the zombi count
            {
                zombiCount *= 2;
            }

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