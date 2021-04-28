using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasingPlayer : StateMachineBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;

    [Header("References")] 
    [SerializeField] private AudioClip chasingClip;

    [Header("Settings")]
    [SerializeField] private float agentChasingSpeed;
    [SerializeField] [Range(0, 20)] private float visionDistanceIncrease;
    [SerializeField] [Range(0, 80)] private float visionRangeIncrease;
    
    private float _agentPatrollingSpeed;

    private FieldOfView _fov;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        _agentPatrollingSpeed = agent.speed;

        agent.speed = agentChasingSpeed;
        _fov = agent.GetComponent<FieldOfView>();
        
        _fov.viewRadius += visionDistanceIncrease;
        _fov.viewAngle += visionRangeIncrease;

        player = GameObject.FindWithTag("Player");
        
        if (!AudioHandler.INSTANCE.IsPlayingClip(chasingClip))
            AudioHandler.INSTANCE.SwapTrack(chasingClip);
    }
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(player.transform.position);
    }
    
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.speed = _agentPatrollingSpeed;
        
        _fov.viewRadius -= visionDistanceIncrease;
        _fov.viewAngle -= visionRangeIncrease;
        
        agent.SetDestination(player.transform.position);
    }
}
