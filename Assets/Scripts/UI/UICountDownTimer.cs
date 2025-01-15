using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICountDownTimer : MonoBehaviour, IDependency<RaceStateTracker>
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Timer timer;
    private RaceStateTracker raceStateTracker;

    private void Start()
    {
        raceStateTracker.PreparationStarted += OnPreparationStarted;
        raceStateTracker.Started += OnRaceStarted;
        text.enabled = false;
    }

    private void OnDestroy()
    {
        raceStateTracker.PreparationStarted -= OnPreparationStarted;
        raceStateTracker.Started -= OnRaceStarted;
    }

    private void OnRaceStarted()
    {
        text.enabled = false;
        enabled = false;
    }

    private void OnPreparationStarted()
    {
        text.enabled = true;
        enabled = true;
    }

    private void Update()
    {
        text.text = raceStateTracker.CountDownTimer.Value.ToString("F0");

        if (text.text == "0")
            text.text = "Go!";
    }

    public void Construct(RaceStateTracker obj)
    {
        raceStateTracker = obj;
    }
}
