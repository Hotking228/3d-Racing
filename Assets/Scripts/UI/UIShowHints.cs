using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShowHints : MonoBehaviour, IDependency<RaceStateTracker>
{
    private RaceStateTracker raceStateTracker;
    [SerializeField] private GameObject hintObject;

    public void Construct(RaceStateTracker obj)
    {
        raceStateTracker = obj;

    }

    private void Start()
    {
        raceStateTracker.PreparationStarted += OnPreparationStarted;
        hintObject.SetActive(true);
    }



    private void OnPreparationStarted()
    {
        hintObject.SetActive(false);
    }

    private void OnDestroy()
    {
        raceStateTracker.PreparationStarted -= OnPreparationStarted;

    }





}
