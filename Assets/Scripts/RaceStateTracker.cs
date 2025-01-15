using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum RaceState
{
    Preparation,
    CountDown,
    Race,
    Pass
}

public class RaceStateTracker : MonoBehaviour, IDependency<TrackPointCurcuit>
{
    public event UnityAction PreparationStarted;
    public event UnityAction Started;
    public event UnityAction Completed;
    public event UnityAction<TrackPoint> TrackPointPassed;
    public event UnityAction<int> LapCompleted;


    private TrackPointCurcuit trackPointCircuit;
    [SerializeField] private Timer countDownTimer;
    public Timer CountDownTimer => countDownTimer;
    [SerializeField] private int lapsToComplete;



    private RaceState state;
    public RaceState State => state;

    private void StartState(RaceState state)
    {
        this.state = state;
    }


    private void Start()
    {
        StartState(RaceState.Preparation);

        countDownTimer.enabled = false;


        countDownTimer.Finished += OnCountDownTimerFinished;

        trackPointCircuit.TrackPointTriggered += OnTrackPointTriggered;
        trackPointCircuit.LapCompleted += OnLapCompleted;
    }

    private void OnCountDownTimerFinished()
    {
        StartRace();
    }

    private void OnLapCompleted(int lapAmount)
    {
        if (trackPointCircuit.Type == TrackType.Sprint)
        {
            CompleteRace();
        }

        if (trackPointCircuit.Type == TrackType.Circular)
        {
            if (lapAmount == lapsToComplete)
            {
                CompleteRace();
            }
            else
            {
                CompleteLap(lapAmount);
            }
        }

    }



    public void LaunchPreparationStart()
    {
        if (state != RaceState.Preparation) return;
        StartState(RaceState.CountDown);

        countDownTimer.enabled = true;
        PreparationStarted?.Invoke();
    }

    private void StartRace()
    {
        if (state != RaceState.CountDown) return;
        StartState(RaceState.Race);
        Started?.Invoke();
    }
    private void CompleteRace()
    {
        if (state != RaceState.Race) return;
        StartState(RaceState.Pass);
        Completed?.Invoke();
    }
    private void CompleteLap(int lapAmount)
    {
        LapCompleted?.Invoke(lapAmount);
    }

    private void OnDestroy()
    {
        trackPointCircuit.TrackPointTriggered -= OnTrackPointTriggered;
        trackPointCircuit.LapCompleted -= OnLapCompleted;
        countDownTimer.Finished -= OnCountDownTimerFinished;
    }
    private void OnTrackPointTriggered(TrackPoint trackPoint)
    {
        TrackPointPassed?.Invoke(trackPoint);
    }


  

    public void Construct(TrackPointCurcuit obj)
    {
        trackPointCircuit = obj;
    }
}
