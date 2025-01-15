using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu]
public class AudioMuxerfloatSetting : Setting
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string nameSetting;

    [SerializeField] private float virtualStep;
    [SerializeField] private float minRealValue;
    [SerializeField] private float maxRealValue;

    private float currentValue = 0;


    [SerializeField] private float minVirtualValue;
    [SerializeField] private float maxVirtualValue;

    public override bool isMinValue { get => currentValue == minRealValue; }

    public override bool isMaxValue { get => currentValue == maxRealValue; }
    public override void SetNextValue()
    {
        AddValue(Mathf.Abs(maxRealValue - minRealValue) / virtualStep);
    }

    public override void SetPreviousValue()
    {
        AddValue(-Mathf.Abs(maxRealValue - minRealValue) / virtualStep);
    }

    public override string GetStringValue()
    {
        return Mathf.Lerp(minVirtualValue, maxVirtualValue, (currentValue - minRealValue) / (maxRealValue - minRealValue)).ToString();
    }

    public override object GetValue()
    {
        return currentValue;
    }

    private void AddValue(float value)
    {
        currentValue += value;
        currentValue = Mathf.Clamp(currentValue, minRealValue, maxRealValue);
    }

    public override void Apply()
    {
        audioMixer.SetFloat(nameSetting,currentValue);
        Save();
    }

    public override void Load()
    {
        currentValue = PlayerPrefs.GetFloat(title, 0);
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(title, currentValue);
    }
}
