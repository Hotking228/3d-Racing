using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRaceRecordTime : MonoBehaviour, IDependency<RaceResultTime>, IDependency<RaceStateTracker>
{
    [SerializeField] private GameObject goldRecordObject;
    [SerializeField] private GameObject playerRecordObject;
    [SerializeField] private TextMeshProUGUI goldRecordTime;
    [SerializeField] private TextMeshProUGUI playerRecordTime;


    private RaceResultTime raceResultTime;
    private RaceStateTracker raceStateTracker;

    public void Construct(RaceResultTime obj)
    {
        raceResultTime = obj;
    }

    public void Construct(RaceStateTracker obj)
    {
        raceStateTracker = obj;
    }


    private void Start()
    {
        raceStateTracker.Started += OnRaceStart;
        raceStateTracker.Completed += OnRaceCompleted;

        goldRecordObject.SetActive(false);
        playerRecordObject.SetActive(false);
    }

    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStart;
        raceStateTracker.Completed -= OnRaceCompleted;
    }

    private void OnRaceCompleted()
    {
        goldRecordObject.SetActive(false);
        playerRecordObject.SetActive(false);
    }

    private void OnRaceStart()
    {
        if (raceResultTime.PlayerRecordTime > raceResultTime.GoldTime || !raceResultTime.IsRecordWasSet)
        {
            goldRecordObject.SetActive(true);
            goldRecordTime.text = StringTime.SecondToTimeString(raceResultTime.GoldTime);
        }

        if (raceResultTime.PlayerRecordTime != 0)
        {
            playerRecordObject.SetActive(true);
            playerRecordTime.text = StringTime.SecondToTimeString(raceResultTime.PlayerRecordTime);
        }
    }
}
