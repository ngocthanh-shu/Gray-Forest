using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;
using System;
using System.Net.Http.Headers;

public class UIManager : MonoBehaviour
{ 
    [SerializeField] public Slider HealthBar;
    [SerializeField] public TMP_Text Score;
    [SerializeField] public GameObject EndgameAlter;
    [SerializeField] public TMP_Text YourScore;
    public Action<int> ChangeValueScore;
    public Action<int> ChangeHealthBar;
    public Action<int> EndGameScore;

    private static UIManager instance;

    public static UIManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this) 
        {
            Destroy(instance);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ChangeValueScore += UpdateScore;
        ChangeHealthBar += UpdateHealthBar;
        EndGameScore += ShowAlter;
    }

    private void ShowAlter(int value)
    {
        EndgameAlter.SetActive(true);
        YourScore.text = "Your score: " + value.ToString();
    }

    private void UpdateScore(int value)
    {
        Score.text = value.ToString();
    }

    private void UpdateHealthBar(int value)
    {
        if (value < 0) value =0;

        HealthBar.value = value;
    }

    public void Initialized(Player player)
    {
        HealthBar.value = HealthBar.maxValue = player.Health;
        Score.text = "0";
    }

    
}
