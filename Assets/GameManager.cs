using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public MonsterCheck monsterCheck;
    
    public Action<int> AddScoreEvent;
    public Action EndGameEvent;

    public int Score;

    private static GameManager instance;

    public static GameManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if(instance != null && instance != this) 
        {
            Destroy(gameObject);
            return;
        }        
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Score = 0;

        AddScoreEvent += AddScore;
        EndGameEvent += EndGame;
    }

    private void EndGame()
    {
        Time.timeScale = 0f;

        UIManager.Instance.EndGameScore(Score);
    }

    private void AddScore(int value)
    {
        Score += value;
        monsterCheck.DecreaseMonsterInZone();
        UIManager.Instance.ChangeValueScore.Invoke(Score);
    }
}
