using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLevelCondition : LevelCondition
{
    protected override void OnLevelCompleted()
    {
        isCompleted = true;

        base.OnLevelCompleted();
    }
}
