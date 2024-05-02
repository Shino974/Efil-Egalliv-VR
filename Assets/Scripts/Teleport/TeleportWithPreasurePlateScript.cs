using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TeleportScript))]
[Tooltip("Teleport the player to a specific position when the player enters trigger something")]
public class TeleportWithPreasurePlateScript : MonoBehaviour
{
    [Tooltip("The name of the scene where the player will be teleported")]
    [SerializeField] private string targetSceneName;
    
    [Tooltip("The position where the player will be teleported")]
    [SerializeField] private Vector3 targetPosition;
    
    private TeleportScript _teleportScript;

    private void Awake()
    {
        _teleportScript.GetComponent<TeleportScript>();
        if (_teleportScript == null)
            Debug.LogError("Teleport script not found");
    }

    private void SetVariables()
    {
        _teleportScript.SetTargetSceneName(targetSceneName);
        _teleportScript.SetTargetPosition(targetPosition);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetVariables();
            _teleportScript.Teleport();
        }
    }
}
