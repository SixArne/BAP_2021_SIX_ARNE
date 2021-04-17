using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class PatrolBehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private AudioSource agentSound;
    
    private ConnectedWaypoint _currentWaypoint;
    private ConnectedWaypoint _previousWaypoint;

    private bool _isTravelling = false;
    private int _waypointsVisited = 0;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent == null)
        {
            agent = animator.gameObject.GetComponent<NavMeshAgent>();
            agentSound = animator.gameObject.GetComponent<AudioSource>();
        }
        
        agentSound.Play();
            

        var closestPoint = FindClosestPatrolPoint();

        _currentWaypoint = closestPoint;
        _previousWaypoint = closestPoint;
        _isTravelling = true;
        
        _waypointsVisited++;
        
        agent.SetDestination(closestPoint.transform.position);
        
        AudioHandler.INSTANCE.ReturnToDefault();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_isTravelling && agent.remainingDistance <= 1.0f)
        {
            SetDestination();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     
    // }

    void SetDestination()
    {
        if (_waypointsVisited > 0)
        {
            ConnectedWaypoint nextWaypoint = _currentWaypoint.NextWaypoint(_previousWaypoint);
            _previousWaypoint = _currentWaypoint;
            _currentWaypoint = nextWaypoint;
        }

        Vector3 targetVector = _currentWaypoint.transform.position;
        agent.SetDestination(targetVector);
        _isTravelling = true;
    }

    ConnectedWaypoint FindClosestPatrolPoint()
    {
        List<ConnectedWaypoint> patrolPoints = 
            GameObject.FindGameObjectsWithTag("Waypoint").Select(x => x.GetComponent<ConnectedWaypoint>()).ToList();
        
        if (patrolPoints.Count == 0) Debug.LogError("Not enough patrolpoints found");

        ConnectedWaypoint closest = patrolPoints[0];
        float closestDistance = Vector3.Distance(agent.transform.position, patrolPoints[0].transform.position);
        
        foreach (var point in patrolPoints)
        {
            float distance = Vector3.Distance(agent.transform.position, point.transform.position);

            if (distance < closestDistance)
            {
                closest = point;
                closestDistance = distance;
            }
        }

        return closest;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
