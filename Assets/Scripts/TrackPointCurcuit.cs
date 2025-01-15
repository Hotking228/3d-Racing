using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum TrackType
{
    Circular,
    Sprint
}

public class TrackPointCurcuit : MonoBehaviour
{
    public event UnityAction<TrackPoint> TrackPointTriggered;
    public event UnityAction<int> LapCompleted;
    [SerializeField] private TrackType type;
    public TrackType Type=>type;
    private TrackPoint[] points;
    private int lapsCompleted = -1;
    private void Awake()
    {
        BuildCurcuit();
    }
    private void Start()
    {
        
        LapCompleted += (t) => { Debug.Log("Lap Completed"); };
        for (int i = 0; i < points.Length; i++)
        {
            points[i].Triggered += OnTrackPointTriggered;
        }
        points[0].AssignAsTarget();
    }


    [ContextMenu(nameof(BuildCurcuit))]
    private void BuildCurcuit()
    {
        points = TrackCurcuitBuilder.Build(transform, type);
    }

    private void OnDestroy()
    {
            for (int i = 0; i < points.Length; i++)
            {
                points[i].Triggered -= OnTrackPointTriggered;
            }
    }


    private void OnTrackPointTriggered(TrackPoint trackPoint)
    {
        if (!trackPoint.IsTarget) return;
        TrackPointTriggered?.Invoke(trackPoint);
        trackPoint.Passed();
        trackPoint.Next?.AssignAsTarget();

        if (trackPoint.IsLast)
        {
            lapsCompleted++;
            if (type == TrackType.Sprint)
            {
                LapCompleted?.Invoke(lapsCompleted);    
            }

            if (type == TrackType.Circular)
            {
                if (lapsCompleted > 0)
                {
                    LapCompleted?.Invoke(lapsCompleted);
                }
            }
        }
    }

   
}
