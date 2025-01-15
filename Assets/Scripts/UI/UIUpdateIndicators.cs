using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdateIndicators : MonoBehaviour
{
    private Car car;
    [SerializeField]private TextMeshProUGUI speedText;
    [SerializeField] private Image rpmImage;
    [SerializeField] private TextMeshProUGUI gearText;
    private void Start()
    {
        car = GetComponent<Car>();
    }

    private void Update()
    {
        gearText.text = (car.SelectedGearIndex + 1).ToString();
        rpmImage.fillAmount = car.EngineRpmNormalized;
        speedText.text = car.LinearVelocity.ToString("0");
    }
}
