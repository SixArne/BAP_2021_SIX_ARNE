using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDoorRaycast : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int rayLength = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludeLayerName = null;
    [SerializeField] private Vector3 rayCastOffset;

    private DoorController raycastedObj;
    private const string interactableTag = "InteractiveObject";
    
    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;
        
        if (Physics.Raycast(transform.position + rayCastOffset, fwd, out hit, rayLength, mask))
        {
            
            if (hit.collider.CompareTag(interactableTag))
            {
                raycastedObj = hit.collider.gameObject.GetComponent<DoorController>();

                raycastedObj.PlayAnimation();

                // automatic door closing
                StartCoroutine(CloseDoor());

            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;


        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(transform.position + rayCastOffset, direction);
    }

    IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(2);
        
        raycastedObj.PlayAnimation();
    }
}
