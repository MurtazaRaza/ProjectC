using System;
using GameEvents;
using TMPro;
using UnityEngine;
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

    private void Awake()
    {
        backButton.onClick.AddListener(() =>
        {
            BroadcastSystem.OnBackToMainMenu?.Invoke();
        });
        
        clearButton.onClick.AddListener(() =>
        {
            PlayerPrefs.DeleteKey(StateSerializer.CARD_GRID);
            PlayerPrefs.DeleteKey(StateSerializer.HAS_SAVED_STATE); 
        });
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void UpdateComboText(int combo)
    {
        comboText.text = combo.ToString();
    }

}
