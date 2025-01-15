using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class GraphicsQuality : Setting
{
    private int currentLevelIndex = 0;

    public override bool isMinValue { get => currentLevelIndex == 0; }
    public override bool isMaxValue { get => currentLevelIndex == QualitySettings.names.Length - 1; }

    public override void SetNextValue()
    {
        if (!isMaxValue)
            currentLevelIndex++;

    }

    public override void SetPreviousValue()
    {
        if(!isMinValue)
            currentLevelIndex--;
    }

    public override object GetValue()
    {
        return QualitySettings.names[currentLevelIndex];
    }
    public override string GetStringValue()
    {
        return QualitySettings.names[currentLevelIndex];
    }


    public override void Apply()
    {
        QualitySettings.SetQualityLevel(currentLevelIndex);
        Save();
    }

    public override void Load()
    {
        currentLevelIndex = PlayerPrefs.GetInt(title, 0);
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(title, currentLevelIndex);
    }
}
