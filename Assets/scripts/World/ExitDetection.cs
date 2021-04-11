using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDetection : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private NotificationMenu _notificationMenu;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_scoreManager.HasCollectedAllObjects()){
                SceneManager.LoadScene("MenuScreen");
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                _notificationMenu.GetAll();
            }
        }
    }
}
