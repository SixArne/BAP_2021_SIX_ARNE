using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FieldOfView : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] [Range(0, 360)] public float viewRadius;
    [SerializeField] [Range(0, 360)] public float viewAngle;
    [SerializeField] public Color debugDetectionColor;

    [Header("References")] 
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMove player;
    
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
    
    // AI Game over collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("MenuScreen");
            Cursor.lockState = CursorLockMode.None;
        }
    }
    
    void FindVisibleTargets()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        if (targetsInViewRadius.Length == 0)
        {
            animator.SetBool("hasLineOfSight", false);
            animator.SetBool("hasHeardSound", false);
            
            return;
        }
        
        foreach (Collider target in targetsInViewRadius)
        {
            Transform t = target.transform;
            Vector3 dirTarget = (t.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, t.position);

                if (!Physics.Raycast(transform.position, dirTarget, dstToTarget, obstacleMask))
                {
                    animator.SetBool("hasLineOfSight", true);
                }
                else
                {
                    // In this case something block the line of sight, but sound should still be audible
                    animator.SetBool("hasHeardSound", TargetIsAudible());
                    animator.SetBool("hasLineOfSight", false);
                }
            }
            else
            {
                animator.SetBool("hasHeardSound", TargetIsAudible());
            }
        }
    }

    bool TargetIsAudible()
    {
        return (player.IsAudible);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = debugDetectionColor;
        Gizmos.DrawSphere(transform.position, viewRadius);
    }

    public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FindTargetsWithDelay(.2f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
