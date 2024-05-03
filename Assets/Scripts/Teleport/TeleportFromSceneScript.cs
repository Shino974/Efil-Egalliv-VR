using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TeleportFromSceneScript : MonoBehaviour
{
    private string _targetSceneName; // The name of the scene to teleport to
    private Transform _targetPosition; // The position in the new scene to teleport to

    public void SetTargetSceneName(string sceneName)
    {
        _targetSceneName = sceneName;
    }

    public void SetTargetPosition(Transform position)
    {
        _targetPosition = position;
    }

    public void Teleport()
    {
        SceneManager.LoadScene(_targetSceneName);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = _targetPosition.position;
    }
}