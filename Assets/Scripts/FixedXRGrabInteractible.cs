using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FixedXRGrabInteractible : XRGrabInteractable
{
    [SerializeField] private Transform LeftHandAttachTransform;    
    [SerializeField] private Transform RightHandAttachTransform;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("LeftHand"))
        {
            attachTransform = LeftHandAttachTransform;
        }
        if (args.interactorObject.transform.CompareTag("RightHand"))
        {
            attachTransform = RightHandAttachTransform;
        }
        
        Debug.Log(args.interactorObject.transform.gameObject);
        base.OnSelectEntered(args);
    }
}
