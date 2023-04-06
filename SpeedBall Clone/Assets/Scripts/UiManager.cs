using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        GameManager.OnGameOver += OpenGameOverPanel;
        GameManager.OnWinTheGame += OpenWinPanel;
    }
    private void OnDisable()
    {
        GameManager.OnGameOver -= OpenGameOverPanel;
        GameManager.OnWinTheGame -= OpenWinPanel;
    }
    private void Update()
    {
        scoreText.text = GameManager.Instance.Score.ToString();
    }
    void OpenGameOverPanel()
    {
        gameOverPanel.SetActive(true);     
    }
    void OpenWinPanel()
    {
        winPanel.SetActive(true);
    }
    
}
