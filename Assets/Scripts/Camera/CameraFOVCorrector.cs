using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOVCorrector : CarCameraComponent
{
    [SerializeField] private float minFieldOfView;
    [SerializeField] private float maxFieldOfView;

    private float defaultFov;

    private void Start()
    {
        defaultFov = camera.fieldOfView;
    }

    private void Update()
    {
        camera.fieldOfView = Mathf.Lerp(minFieldOfView, maxFieldOfView, car.LinearVelocityNormalized);
    }
}
