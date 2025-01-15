using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRespawner : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<Car>, IDependency<CarInputControl>
{

    private TrackPoint respawnTrackPoint;
    [SerializeField] private Vector3 respawnOffset;
    private RaceStateTracker raceStateTracker;
    private Car car;
    private CarInputControl carInputControl;

    public void Construct(RaceStateTracker obj)
    {
        raceStateTracker = obj;
    }

    public void Construct(Car obj)
    {
        car = obj;
    }

    public void Construct(CarInputControl obj)
    {
        carInputControl = obj;
    }

    private void Start()
    {
        raceStateTracker.TrackPointPassed += OnTrackPointPassed;
    }

    private void OnDestroy()
    {
        raceStateTracker.TrackPointPassed -= OnTrackPointPassed;
    }

    private void OnTrackPointPassed(TrackPoint point)
    {
        respawnTrackPoint = point;  
    }
    public void Respawn()
    {

        if (respawnTrackPoint == null) return;

        if (raceStateTracker.State != RaceState.Race) return;


        car.Respawn(respawnTrackPoint.transform.position - respawnOffset, respawnTrackPoint.transform.rotation);
        carInputControl.Reset();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }
}
