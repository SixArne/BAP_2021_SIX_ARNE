using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConnectedWaypoint : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private float nodeCost = 50f;
    [SerializeField] private float _debugDrawRadius = 5f;

    [SerializeField]
    List<ConnectedWaypoint> connections;

    [SerializeField]
    public float _cost;

    private void Start()
    {
        foreach (var con in connections)
        {
            if (!con.connections.Contains(this))
            {
                con.connections.Add(this);
            }
        }
    }

    private void Update()
    {
        if (_cost > 0) _cost -= Time.deltaTime;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.3f);

        Gizmos.color = Color.green;
        foreach(ConnectedWaypoint pp in connections)
        {
            Gizmos.DrawLine(transform.position, pp.transform.position);
        }
    }

    public ConnectedWaypoint NextWaypoint(ConnectedWaypoint previousWaypoint)
    {
        if (connections.Count == 0)
        {
            Debug.LogError("Insufficient waypoint count");
            return null;
        }
        else if (connections.Count == 1 && connections.Contains(previousWaypoint))
        {
            return previousWaypoint;
        }
        else
        {
            var choices = new List<ConnectedWaypoint>(connections);
            choices.Remove(previousWaypoint);

            var minCostWaypoint = choices[0];

            foreach (var point in choices)
            {
                if (point._cost < minCostWaypoint._cost)
                {
                    minCostWaypoint = point;
                }
            }

            minCostWaypoint._cost = nodeCost;
            return minCostWaypoint;
        }
    }
}