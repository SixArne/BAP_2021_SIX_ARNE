using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject map;

    void Update()
    {
        if(InputHandler.HasOpenedMap)
        {
            map.SetActive(!map.activeInHierarchy);
        }
    }
}
