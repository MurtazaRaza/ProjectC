using System;
using GameEvents;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UiManager : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI scoreText;
    [SerializeField] 
    private TextMeshProUGUI comboText;
    [SerializeField] 
    private Button backButton;
    [SerializeField] 
    private Button clearButton;

    [SerializeField] 
    private GameObject gameOverPanel;
    [SerializeField] 
    private Button gameOverMainMenuButton;

    [SerializeField] 
    private TextMeshProUGUI finalScoreText;

    private int _cachedScore;

    private void Awake()
    {
        backButton.onClick.AddListener(MoveToMainMenu);
        
        clearButton.onClick.AddListener(() =>
        {
            PlayerPrefs.DeleteKey(StateSerializer.CARD_GRID);
            PlayerPrefs.DeleteKey(StateSerializer.HAS_SAVED_STATE); 
        });
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
        _cachedScore = score;
    }

    public void UpdateComboText(int combo)
    {
        comboText.text = combo.ToString();
    }

    public void DisplayEndGameUi()
    {
        gameOverMainMenuButton.onClick.AddListener(MoveToMainMenu);
        finalScoreText.text = _cachedScore.ToString();
        gameOverPanel.SetActive(true);
    }
    
    void MoveToMainMenu()
    {
        BroadcastSystem.OnBackToMainMenu?.Invoke();
        SceneManager.LoadScene("MainMenuScene");
    }

}
