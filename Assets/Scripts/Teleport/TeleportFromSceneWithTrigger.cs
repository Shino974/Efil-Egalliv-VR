using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(TeleportFromSceneScript), typeof(BoxCollider))]
[Tooltip("Teleport the player to a specific position when the player enters trigger something")]
public class TeleportFromSceneWithTrigger : MonoBehaviour
{
    [Tooltip("The name of the scene where the player will be teleported")]
    [SerializeField] private string targetSceneName;
    
    [Tooltip("The position where the player will be teleported")]
    [SerializeField] private Transform targetPosition;
    
    private TeleportFromSceneScript _teleportFromSceneScript;
    private BoxCollider _boxCollider;
    
    private void Awake()
    {
        _teleportFromSceneScript = GetComponent<TeleportFromSceneScript>();
        if (_teleportFromSceneScript == null)
            Debug.LogError("Teleport script not found" + this.gameObject.name);
        _boxCollider = GetComponent<BoxCollider>();
        if (_boxCollider == null)
            Debug.LogError("Box collider not found" + this.gameObject.name);
        _boxCollider.isTrigger = true;
    }

    private void SetVariables()
    {
        _teleportFromSceneScript.SetTargetSceneName(targetSceneName);
        _teleportFromSceneScript.SetTargetPosition(targetPosition);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetVariables();
            _teleportFromSceneScript.Teleport();
        }
    }
}
