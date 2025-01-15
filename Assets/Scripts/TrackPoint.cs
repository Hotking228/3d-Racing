using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrackPoint : MonoBehaviour
{
    public UnityAction<TrackPoint> Triggered;
    public TrackPoint Next;
    public bool IsFirst;
    public bool IsLast;
    protected virtual void OnPassed() { }
    protected virtual void OnAssignedAsTarget() { }
    protected bool isTarget;
    public bool IsTarget => isTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.GetComponent<Car>() == null) return;
        Triggered?.Invoke(this);
    }
    public void Passed()
    {
        isTarget = false;
        OnPassed();
    }
    public void AssignAsTarget()
    {
        isTarget = true;
        OnAssignedAsTarget();
    }

    public void Reset()
    {
        Next = null;
        IsFirst = false;
        IsLast = false;
    }
}
