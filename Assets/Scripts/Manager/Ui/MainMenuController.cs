using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] 
    private GameObject levelsMenuPanel;
    [SerializeField] 
    private GameObject mainMenuPanel;
    [SerializeField] 
    private Button[] levelButtons;
    [SerializeField] 
    private Button playButton;
    [SerializeField] 
    private Button backToMainMenuButton;
    [SerializeField] 
    private AllLevels allLevels;
    [SerializeField] 
    private LevelData currentLevel;

    private void Awake()
    {
        // Main menu button setup
        playButton.onClick.AddListener(() =>
        {
            mainMenuPanel.gameObject.SetActive(false);
            levelsMenuPanel.gameObject.SetActive(true);
        });

        // Levels menu setup
        backToMainMenuButton.onClick.AddListener(() =>
        {
            mainMenuPanel.gameObject.SetActive(true);
            levelsMenuPanel.gameObject.SetActive(false);
        });

        for (var index = 0; index < levelButtons.Length; index++)
        {
            var index1 = index;
            levelButtons[index].onClick.AddListener(() =>
            {
                OpenLevel(index1 + 1);
            });
        }
        
    }

    private void Start()
    {
        UnlockLevels();
    }

    private void UnlockLevels()
    {
        foreach (var levelButton in levelButtons)
        {
            levelButton.enabled = false;
        }

        int levelsUnlocked = StateSerializer.GetNumberOfLevelsCompleted();

        for (int i = 0; i <= levelsUnlocked; i++)
        {
            levelButtons[i].enabled = true;
        }
    }

    private void OpenLevel(int levelNumber)
    {
        LevelData levelData = allLevels.allLevelsData.FirstOrDefault(level => level.levelNumber == levelNumber);
            
        currentLevel.SetLevelData(levelData);

        SceneManager.LoadScene("GameplayScene");

    }
    
    
}
