using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDetection : MonoBehaviour
{
    [SerializeField]
    private ScoreManager _scoreManager;

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
                Debug.Log("Please collect all objects first");
            }
        }
    }
}
