using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject collectablePrefab;
    [SerializeField] private GameObject mapIconPrefab;
    [SerializeField] private GameObject childMap;
    [SerializeField] private GameObject map;
    [SerializeField] private String tagName;

    [SerializeField] public int spawns = 9;

    private List<GameObject> spawnpoints;
    private GameObject[] mapSpawnpoints;

    private List<GameObject> selectedMapSpawnpoints = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        mapSpawnpoints = GameObject.FindGameObjectsWithTag("map_spawnpoint");
        spawnpoints = GameObject.FindGameObjectsWithTag("dev_spawnpoint").ToList();

        List<GameObject> choices = mapSpawnpoints.ToList();

        while (selectedMapSpawnpoints.Count < spawns)
        {
            int r = Random.Range(0, choices.Count - 1);

            if (!selectedMapSpawnpoints.Exists(o => o == choices[r]))
            {
                selectedMapSpawnpoints.Add(choices[r]);
                choices.RemoveAt(r);
            }
        }

        Init();
    }

    // Update is called once per frame
    void Init()
    {
        foreach (GameObject spawnpoint in selectedMapSpawnpoints)
        {
            Classroom b = spawnpoint.GetComponent<MapLocation>().classroom;

            GameObject a = Instantiate(mapIconPrefab, spawnpoint.transform.position, spawnpoint.transform.rotation);
            a.GetComponent<MapLocation>().classroom = b;
            
            //a.transform.localScale = new Vector3(0.0416183472f, 0.0416183472f, 0.0416183472f);
            a.transform.SetParent(map.transform);
            a.tag = tagName;
            a.name = Random.Range(0, 100).ToString();

            // find spawnpoints with classroom
            spawnpoint.GetComponent<MapLocation>().classroom = b;
            
            List<GameObject> possibleSpawnpoints = spawnpoints.FindAll(sp => sp.GetComponent<MapLocation>().classroom == b);

            int r = Random.Range(0, possibleSpawnpoints.Count);
            
            GameObject spawnedCollectable = Instantiate(collectablePrefab, possibleSpawnpoints[r].transform.position, possibleSpawnpoints[r].transform.rotation);
            spawnedCollectable.GetComponent<MapLocation>().classroom = b;
        }
    }
}
