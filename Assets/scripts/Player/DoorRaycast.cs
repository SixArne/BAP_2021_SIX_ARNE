using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorRaycast : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int rayLength = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludeLayerName = null;

    [Header("References")] 
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private Animator animator;

    private DoorController raycastedObj;

    //[SerializeField] private KeyCode openDoorKey = KeyCode.Mouse0;

    [SerializeField] private Image crosshair = null;
    private bool isCrosshairActive;
    private bool doOnce;

    private const string interactableTag = "InteractiveObject";

    private void Awake()
    {
        if (crosshair != null) crosshair.enabled = false;
    }

    private void Update()
    {  
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, mask))
        {
            if (hit.collider.CompareTag(interactableTag))
            {
                if (!doOnce)
                {
                    raycastedObj = hit.collider.gameObject.GetComponent<DoorController>();
                    CrosshairChange(true);
                }

                isCrosshairActive = true;
                crosshair.enabled = true;
                doOnce = true;
                
                if (InputHandler.HasClickedThisFrame)
                {
                    animator.SetTrigger("madeSound");
                    raycastedObj.PlayAnimation();
                }
            }
        }

        else
        {
            if (isCrosshairActive)
            {
                CrosshairChange(false);
                doOnce = false;
            }
        }
    }

    void CrosshairChange(bool on)
    {
        if (on && !doOnce)
        {
            crosshair.color = Color.red;
        }
        else
        {
            crosshair.color = Color.white;
            crosshair.enabled = false;
            isCrosshairActive = false;
        }
    }
}
