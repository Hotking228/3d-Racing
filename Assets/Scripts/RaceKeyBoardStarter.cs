using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceKeyBoardStarter : MonoBehaviour, IDependency<RaceStateTracker>
{
    private RaceStateTracker raceStateTracker;

    public void Construct(RaceStateTracker obj)
    {
        raceStateTracker = obj;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            raceStateTracker.LaunchPreparationStart();
        }
    }
}
