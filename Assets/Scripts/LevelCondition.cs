using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class LevelCondition : MonoBehaviour, IDependency<RaceStateTracker>
{
    private RaceStateTracker raceStateTracker;
    [SerializeField] protected int currentLevel;
    public int CurrentLevel => currentLevel;
    protected bool isCompleted;
    public bool IsCompleted => isCompleted;


    public event UnityAction<bool> Completed;
    public void Construct(RaceStateTracker obj)
    {
        raceStateTracker = obj;
    }

    private void Awake()
    {
        raceStateTracker.Completed += OnLevelCompleted;
    }

    private void OnDestroy()
    {
        raceStateTracker.Completed -= OnLevelCompleted;
    }

    protected virtual void OnLevelCompleted() { Completed?.Invoke(isCompleted); }
}
