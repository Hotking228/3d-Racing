using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIComletedRace : MonoBehaviour, IDependency<RaceStateTracker>
{
    [SerializeField] private GameObject finalPanel;
    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj)
    {
        raceStateTracker = obj;
    }


    private void Start()
    {
        raceStateTracker.Completed += OnRaceCompleted;
        finalPanel.SetActive(false);
    }

    private void OnRaceCompleted()
    {
        finalPanel.SetActive(true);
    }

    private void OnDestroy()
    {
        raceStateTracker.Completed -= OnRaceCompleted;
    }
}
