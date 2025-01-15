using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITrackTime : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceTimeTracker>
{
    private RaceStateTracker raceStateTracker;



    private RaceTimeTracker raceTimeTracker;

    [SerializeField] private TextMeshProUGUI text;

    public void Construct(RaceTimeTracker obj)
    {
        raceTimeTracker = obj;
    }

    public void Construct(RaceStateTracker obj)
    {
        raceStateTracker = obj;
    }

    private void Start()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Completed += OnRaceCompleted;

        text.enabled = false;
    }

    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Completed -= OnRaceCompleted;
    }

    private void OnRaceStarted()
    {
        text.enabled = true;
        enabled = true;
    }

    private void OnRaceCompleted()
    {
        text.enabled = false;
        enabled = false;
    }

    private void Update()
    {
        text.text = StringTime.SecondToTimeString(raceTimeTracker.CurrrentTime);
    }
}
