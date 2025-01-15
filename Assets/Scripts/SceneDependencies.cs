using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDependencies : Dependencies
{
    [SerializeField] private RaceStateTracker raceStateTracker;
    [SerializeField] private RaceTimeTracker raceTimeTracker;
    [SerializeField] private RaceResultTime raceResultTime;
    [SerializeField] private CarInputControl carInputControl;
    [SerializeField] private Car car;
    [SerializeField] private CarCameraController carCameraController;
    [SerializeField] private TrackPointCurcuit trackPointCurcuit;

    protected override void BindAll(MonoBehaviour monoBehaviourInScene)
    {
        Bind<RaceStateTracker>(raceStateTracker, monoBehaviourInScene);
        Bind<RaceTimeTracker>(raceTimeTracker, monoBehaviourInScene);
        Bind<RaceResultTime>(raceResultTime, monoBehaviourInScene);
        Bind<CarInputControl>(carInputControl, monoBehaviourInScene);
        Bind<Car>(car, monoBehaviourInScene);
        Bind<CarCameraController>(carCameraController, monoBehaviourInScene);
        Bind<TrackPointCurcuit>(trackPointCurcuit, monoBehaviourInScene);
        
    }




    private void Awake()
    {
       FindAllObjectsToBind();
    }
}
