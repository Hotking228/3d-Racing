using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIShowFinishInfo : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceTimeTracker>, IDependency<RaceResultTime>
{
    private RaceStateTracker raceStateTracker;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TextMeshProUGUI resultText;
    private RaceTimeTracker raceTimeTracker; 
    private RaceResultTime raceResultTime; 

    public void Construct(RaceStateTracker obj)
    {
        raceStateTracker = obj;
    }

    public void Construct(RaceTimeTracker obj)
    {
        raceTimeTracker = obj;
    }

    public void Construct(RaceResultTime obj)
    {
        raceResultTime = obj;
    }

    private void Start()
    {
        resultPanel.SetActive(false);
        raceStateTracker.Completed += OnRaceCompleted;
    }


    private void OnDestroy()
    {
        raceStateTracker.Completed += OnRaceCompleted;
    }

    private void OnRaceCompleted()
    {
        resultPanel.SetActive(true);
        resultText.text = $"Текущее время: {StringTime.SecondToTimeString( raceTimeTracker.CurrrentTime)}\n" + (raceResultTime.PlayerRecordTime > 0 ? $"\nЛучшее время: {StringTime.SecondToTimeString(raceResultTime.PlayerRecordTime)}":"");
    }

   
}
