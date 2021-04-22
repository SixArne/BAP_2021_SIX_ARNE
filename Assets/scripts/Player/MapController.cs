using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject map;

    private bool hasOpenedMapLastFrame;

    void Update()
    {
        if(InputHandler.HasOpenedMap && !hasOpenedMapLastFrame)
        {
            map.SetActive(!map.activeInHierarchy);
        }

        hasOpenedMapLastFrame = InputHandler.HasOpenedMap;
    }
}
