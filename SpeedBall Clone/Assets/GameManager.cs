using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static Action OnGameOver;
    public static Action OnWinTheGame;

    public int Score { get; private set; }

    [SerializeField] Ball ball;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        IncreaseScore();     
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void IncreaseScore()
    {
        if (ball.IsLiving)
        {
            Score += Mathf.CeilToInt(ball.Speed * Time.deltaTime);
        }
    }
    
}
