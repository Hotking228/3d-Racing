using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSequnceController : MonoBehaviour
{
    private ShowPossibleLevels showLevels;
    private LevelCondition levelCondition;

    [SerializeField]private int currentOpenedLevel;
    [SerializeField]private int currentLevel;

    private const string saveFilename = "currentLevel";


    private void Awake()
    {
       // Load();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {  
        showLevels = FindAnyObjectByType<ShowPossibleLevels>(FindObjectsInactive.Include);
        levelCondition = FindAnyObjectByType<LevelCondition>(FindObjectsInactive.Include);

        if (showLevels != null)
        {
            Save();
            showLevels.SetCurrentLevel(currentOpenedLevel);
        }
        if (levelCondition != null)
        {
            currentLevel = levelCondition.CurrentLevel;
            levelCondition.Completed += OnLevelCompleted;
        }
    }

    private void OnDestroy()
    {
        if (levelCondition != null)
        {
            levelCondition.Completed -= OnLevelCompleted;
        }
    }
    private void OnLevelCompleted(bool isCompleted)
    {
        if (isCompleted)
        {
            currentOpenedLevel = Mathf.Max(currentLevel + 1, currentOpenedLevel);
        }
    }

    private void SetCurrentLevel(int level)
    {
        currentLevel = level;
    }

    private void Save()
    {
        PlayerPrefs.SetInt(saveFilename, currentOpenedLevel);
    }

    private void Load()
    {
        currentOpenedLevel = PlayerPrefs.GetInt(saveFilename);
    }

}
