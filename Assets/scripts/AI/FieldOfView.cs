using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] [Range(0, 360)] public float viewRadius;
    [SerializeField] [Range(0, 360)] public float viewAngle;

    [Header("References")] 
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;

    public bool playerInView;

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
    
    void FindVisibleTargets()
    {
        playerInView = false;
        
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        foreach (Collider target in targetsInViewRadius)
        {
            Transform t = target.transform;
            Vector3 dirTarget = (t.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, t.position);

                if (!Physics.Raycast(transform.position, dirTarget, dstToTarget, obstacleMask))
                {
                    playerInView = true;
                }
            }
            else
            {
                playerInView = false;
            }
        }
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
        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
