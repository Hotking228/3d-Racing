using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RaceResultTime : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceTimeTracker>
{
    public static string SaveMask = "_player_best_time";

    public event UnityAction ResultUpdated;
    private RaceStateTracker raceStateTracker;
    [SerializeField] private float goldTime;
    public float GoldTime=>goldTime;

    private float playerRecordTime;
    public float PlayerRecordTime => playerRecordTime;
    private float currentTime;
    public float CurrentTime => currentTime;

    public bool IsRecordWasSet => playerRecordTime != 0;



    private RaceTimeTracker raceTimeTracker;
    public void Construct(RaceTimeTracker obj)
    {
        raceTimeTracker = obj;
    }

    public void Construct(RaceStateTracker obj)
    {
        raceStateTracker = obj;
    }

    private void Awake()
    {
        Load();
    }
    private void Start()
    {

        raceStateTracker.Completed += OnRaceCompleted;

    }

    private void OnRaceCompleted()
    {
        float absolutRecord = GetAbsolutRecord();
        if (raceTimeTracker.CurrrentTime < absolutRecord || playerRecordTime == 0)
        {
            playerRecordTime = raceTimeTracker.CurrrentTime;

            Save();
        }
        currentTime = raceTimeTracker.CurrrentTime;
        ResultUpdated?.Invoke();
    }

    public float GetAbsolutRecord()
    {
        if (playerRecordTime < goldTime && playerRecordTime != 0)
        {
            return playerRecordTime;
        }
        else
        {
            return goldTime;
        }
    }

    private void Load()
    {
        playerRecordTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + SaveMask, 0);
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + SaveMask, playerRecordTime);

    }


}
