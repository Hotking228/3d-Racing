using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimeCondition : LevelCondition, IDependency<RaceTimeTracker>
{
    private RaceTimeTracker raceTimeTracker;
    [SerializeField] private float time;
    public void Construct(RaceTimeTracker obj)
    {
        raceTimeTracker = obj;
    }

    protected override void OnLevelCompleted()
    {
        if (raceTimeTracker.CurrrentTime <= time)
        {
            isCompleted = true;
        }
        base.OnLevelCompleted();
    }
}
