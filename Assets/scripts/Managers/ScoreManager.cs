using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [Header("Settings")]
    [SerializeField] private Text scoreText;
    [SerializeField] private int score = 0;
    [SerializeField] private SpawnManager _spawnManager;
    
    [Header("References")]
    [SerializeField] private NotificationMenu _notificationMenu;

    private bool messageSend = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);

        scoreText.text = "0 collected";
    }

    public void Increment()
    {
        score = Mathf.Clamp(score += 1, 0, 10);
        scoreText.text = $"{score.ToString()} collected";
    }

    private void Update()
    {
        if (HasCollectedAllObjects() && messageSend == false)
        {
            _notificationMenu.Leave();
            messageSend = true;
        }
    }

    public bool HasCollectedAllObjects()
    {
        return score == _spawnManager.spawns;
    }
}
