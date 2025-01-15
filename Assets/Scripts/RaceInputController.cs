using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceInputController : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<CarInputControl>
{
    private CarInputControl carControl;
    private RaceStateTracker raceStateTracker;

    private void Start()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Completed += OnRaceFinished;
        carControl.enabled = false;
    }


    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Completed -= OnRaceFinished;
    }

    private void OnRaceStarted()
    {
        carControl.enabled = true;
    }

    private void OnRaceFinished()
    {
        carControl.enabled = false;
        carControl.Stop();
    }

    public void Construct(RaceStateTracker obj)
    {
        raceStateTracker = obj;
    }

    public void Construct(CarInputControl obj)
    {
        carControl = obj;
    }
}
