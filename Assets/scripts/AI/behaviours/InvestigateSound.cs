using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class InvestigateSound : StateMachineBehaviour
{
    /*
     * The AI will run to the player and linger around for the specified duration, if no other state
     * change occurs the AI will result back to the patrol state. If the AI makes eye contact he will go to the
     * chase state.
     */
    
    [Header("References")] 
    [SerializeField] private AudioClip investigatingClip;
    
    [Header("Settings")] 
    [SerializeField] private float investigationTime;
    [SerializeField] private float agentChasingSpeed;

    private float _currentTime;
    private GameObject _player;
    private NavMeshAgent _agent;

    private Transform _lastPosition;
    private float _agentPatrollingSpeed;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // might cause a problem later
        if (_player == null && _agent == null)
        {
            _player = GameObject.FindWithTag("Player");
            _lastPosition = _player.transform;
            _agent = animator.gameObject.GetComponent<NavMeshAgent>();
        }

        _agentPatrollingSpeed = _agent.speed;
        _agent.speed = agentChasingSpeed;
        _agent.SetDestination(_lastPosition.position);
        
        AudioHandler.INSTANCE.SwapTrack(investigatingClip);
    }
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if ((!(Vector3.Distance(_agent.transform.position, _lastPosition.position) <= 5f)))
        {
            _agent.SetDestination(_lastPosition.position);

            return;
        }
        
        if (_currentTime > investigationTime)
        {
            animator.SetTrigger("investigationSoundFinished");
        }

        _currentTime += Time.deltaTime;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        _agent.speed = _agentPatrollingSpeed;
    }
}
