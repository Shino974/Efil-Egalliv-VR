using UnityEngine;

public class ZombiFollowPlayerScript : MonoBehaviour
{
    [Tooltip("The speed at which the zombi moves")]
    [SerializeField] private float speed = 1f;

    private Transform player;

    private void Start()
    {
        // Find the player GameObject by its tag and get its transform
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    private void Update()
    {
        if (player != null && player.gameObject.activeInHierarchy)
        {
            // Make the zombi look at the player
            transform.LookAt(player);

            // Move the zombi towards the player
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }
}