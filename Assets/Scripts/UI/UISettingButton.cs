using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISettingButton : UISelectableButton
{
    [SerializeField] private Setting setting;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI valueText;

    [SerializeField] private Image previousImage;
    [SerializeField] private Image nextImage;

    public void SetNextValue()
    {
        setting?.SetNextValue();
        UpdateInfo(setting);
        setting?.Apply();
    }

    public void SetPreviousValue()
    {
        setting?.SetPreviousValue();
        UpdateInfo(setting);
        setting?.Apply();
    }


    private void Start()
    {
        ApplyProperty(setting);
        
    }


    private void UpdateInfo(Setting property)
    {
        titleText.text = property.Title;
        valueText.text = property.GetStringValue();

        previousImage.enabled = !setting.isMinValue;
        nextImage.enabled = !setting.isMaxValue;
    }

    public void ApplyProperty(Setting property)
    {
        if (property == null) return;

        setting = property;
        UpdateInfo(property);
    }

}
