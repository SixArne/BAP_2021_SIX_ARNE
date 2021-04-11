using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject getAllNotification;
    [SerializeField] private GameObject leaveSchoolNotification;

    [Header("Settings")] [SerializeField] private float delay;

    public void GetAll()
    {
        StopAllCoroutines();
        getAllNotification.SetActive(true);

        StartCoroutine(Notification(getAllNotification));
    }

    public void Leave()
    {
        StopAllCoroutines();
        leaveSchoolNotification.SetActive(true);

        StartCoroutine(Notification(leaveSchoolNotification));
    }

    IEnumerator Notification(GameObject g)
    {
        yield return new WaitForSeconds(delay);
        
        g.SetActive(false);
    }
}
