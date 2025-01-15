using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class ResolutionSettings : Setting
{
    [SerializeField]
    private Vector2Int[] availableResolution = new Vector2Int[]
    {
        new Vector2Int(800, 600),
        new Vector2Int(1280, 720),
        new Vector2Int(1600, 900),
        new Vector2Int(1920, 1080)
    };

    private int currentResolutionIndex = 0;

    public override bool isMinValue { get => currentResolutionIndex == 0; }
    public override bool isMaxValue { get => currentResolutionIndex == availableResolution.Length - 1; }

    public override void SetNextValue()
    {
        if (!isMaxValue)
        {
            currentResolutionIndex++;
        }
    }

    public override void SetPreviousValue()
    {
        if (!isMinValue)
        {
            currentResolutionIndex--;
        }
    }

    public override object GetValue()
    {
        return availableResolution[currentResolutionIndex];
    }
    public override string GetStringValue()
    {
        return availableResolution[currentResolutionIndex].x + "x" + availableResolution[currentResolutionIndex].y;
    }
    public override void Apply()
    {
        Screen.SetResolution(availableResolution[currentResolutionIndex].x, availableResolution[currentResolutionIndex].y, true);
        Save();
    }

    public override void Load()
    {
        currentResolutionIndex = PlayerPrefs.GetInt(title, 0);
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(title, currentResolutionIndex);
    }
}
