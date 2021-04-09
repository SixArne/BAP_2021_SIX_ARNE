using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PiController : MonoBehaviour
{
    static List<GameObject> pinpoints = new List<GameObject>();

    private void Start()
    {
        if (pinpoints.Count == 0)
        {
            pinpoints = GameObject.FindGameObjectsWithTag("map_pinpoint").ToList();
        }
    }

    public void Collect()
    {
        Classroom c = gameObject.GetComponent<MapLocation>().classroom;

        GameObject find = pinpoints.Find(r => r.GetComponent<MapLocation>().classroom == c);
        pinpoints.Remove(find);
        
        print($"{find.name} name");
        
        find.SetActive(false);
        gameObject.SetActive(false);

        ScoreManager.instance.Increment();
    }
}
