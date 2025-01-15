using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public static class TrackCurcuitBuilder
{
    public static TrackPoint[] Build(Transform trackTransform, TrackType type)
    {
        TrackPoint[] points = new TrackPoint[trackTransform.childCount];

        ResetPoints(trackTransform, points);
        MakeLinks(type, points);
        MarkPoint(type, points);

        return points;
    }

    private static void MarkPoint(TrackType type, TrackPoint[] points)
    {
        points[0].IsFirst = true;

        if (type == TrackType.Sprint)
            points[points.Length - 1].IsLast = true;

        if (type == TrackType.Circular)
            points[0].IsLast = true;
    }

    private static void MakeLinks(TrackType type, TrackPoint[] points)
    {
        for (int i = 0; i < points.Length - 1; i++)
        {
            points[i].Next = points[i + 1];
        }

        if (type == TrackType.Circular)
        {
            points[points.Length - 1].Next = points[0];
        }
    }

    private static void ResetPoints(Transform trackTransform, TrackPoint[] points)
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = trackTransform.GetChild(i).GetComponent<TrackPoint>();

            if (points[i] == null)
            {
                Debug.LogError("Theres no TrackPoint script on one of the child objects");
                return;
            }

            points[i].Reset();
        }
    }
}
