using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTimeTracker : MonoBehaviour,IDependency<RaceStateTracker>
{
    private RaceStateTracker raceStateTracker;

    private float currentTime;

    public float CurrrentTime => currentTime;

    public void Construct(RaceStateTracker obj)
    {
        raceStateTracker = obj;
    }


    private void Start()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Completed += OnRaceCompleted;

        enabled = false;
    }

    private void OnRaceCompleted()
    {
        enabled = false;
    }

    private void OnRaceStarted()
    {
        enabled = true;
        currentTime = 0;
    }

    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Completed -= OnRaceCompleted;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
    }
}
