using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class PatrolAI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool _patrolMustWait;
    [SerializeField] private float _totalWaitTime = 3f;
    [SerializeField] private float _switchProbability = 0.2f;

    [Header("References")] 
    [SerializeField] private GameObject player;
    
    private NavMeshAgent _navMeshAgent;
    private ConnectedWaypoint _currentWaypoint;
    private ConnectedWaypoint _previousWaypoint;
    private FieldOfView _fieldOfView;

    private bool _travelling;
    private bool _waiting;
    private float _waitTimer;
    private int _waypointsVisited;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _fieldOfView = GetComponent<FieldOfView>();

        if (_animator == null)
        {
            Debug.LogError("Animator not found");
        }
    }
    
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        if (_navMeshAgent == null)
        {
            Debug.LogError($"The navmesh agent is not attached to {gameObject.name}");
        }
        else
        {
            if (_currentWaypoint == null)
            {
                GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

                if (allWaypoints.Length > 0)
                {
                    while (_currentWaypoint == null)
                    {
                        int random = UnityEngine.Random.Range(0, allWaypoints.Length);
                        ConnectedWaypoint startingPoint = allWaypoints[random].GetComponent<ConnectedWaypoint>();

                        if (startingPoint != null)
                        {
                            _currentWaypoint = startingPoint;
                            _previousWaypoint = _currentWaypoint;
                        }
                    }
                }
                
                /*
                if (allWaypoints.Length == 0) { Debug.LogError("No enough Waypoints"); }
                
                int random = UnityEngine.Random.Range(0, allWaypoints.Length);
                _currentWaypoint = allWaypoints[random].GetComponent<ConnectedWaypoint>();
                */
            }
            else
            {
                Debug.LogError($"Failed to find waypoints in the scene");
            }
        }

        SetDestination();
    }

    private void Update()
    {
        if (_fieldOfView.playerInView)
        {
            Vector3 targetVector = player.transform.position;
            _navMeshAgent.SetDestination(targetVector);

            return;
        }

        if (_travelling && _navMeshAgent.remainingDistance <= 1.0f)
        {
            _travelling = false;
            _waypointsVisited++;

            if (_patrolMustWait)
            {
                _waiting = true;
                
                _animator.Play("Idle");
                
                _waitTimer = 0f;
            }
            else
            {
                {
                    SetDestination();
                }
            }
        }

        if (_waiting)
        {
            _waitTimer += Time.deltaTime; 

            if (_waitTimer >= _totalWaitTime)
            {
                _waiting = false;
                SetDestination();
            }
        }
        else
        {
            _animator.Play("Walking");
        }
    }

    void SetDestination()
    {
        if (_waypointsVisited > 0)
        {
            ConnectedWaypoint nextWaypoint = _currentWaypoint.NextWaypoint(_previousWaypoint);
            _previousWaypoint = _currentWaypoint;
            _currentWaypoint = nextWaypoint;
        }

        Vector3 targetVector = _currentWaypoint.transform.position;
        _navMeshAgent.SetDestination(targetVector);
        _travelling = true;
    }
}
