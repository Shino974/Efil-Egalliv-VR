using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TeleportScript))]
public class TeleportWithPreasurePlateScript : MonoBehaviour
{
    [SerializeField] string targetSceneName;
    [SerializeField] Vector3 targetPosition;
    private TeleportScript _teleportScript;

    private void Awake()
    {
        _teleportScript.GetComponent<TeleportScript>();
        if (_teleportScript == null)
        {
            Debug.LogError("Teleport script not found");
        }
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
