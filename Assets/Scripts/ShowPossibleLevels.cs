using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowPossibleLevels : MonoBehaviour
{
    public int currentLevel;
    private UIRaceButton[] raceButtons;
    [SerializeField] private GameObject levelsContainer;

    public void SetCurrentLevel(int level)
    {
        currentLevel = Mathf.Max(level, currentLevel);
    }

    private void Start()
    {
        raceButtons = levelsContainer.GetComponentsInChildren<UIRaceButton>();
        for (int i = 0; i < currentLevel + 1; i++)
        {
            if (i < raceButtons.Length)
            {
                raceButtons[i].gameObject.SetActive(true);
            }
        }

        for (int i = currentLevel + 1; i < raceButtons.Length; i++)
        {
            raceButtons[i].gameObject.SetActive(false);
        }
    }
}
