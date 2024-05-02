using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportScript : MonoBehaviour
{
    private string targetSceneName; // The name of the scene to teleport to
    private Vector3 targetPosition; // The position in the new scene to teleport to

    public void SetTargetSceneName(string sceneName)
    {
        targetSceneName = sceneName;
    }
    
    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
    }
    
    public void Teleport()
    {
        // Load the target scene
        SceneManager.LoadScene(targetSceneName);

        // Find the player object. This assumes the player object is tagged with "Player".
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Set the player's position to the target position
        player.transform.position = targetPosition;
    }
}
