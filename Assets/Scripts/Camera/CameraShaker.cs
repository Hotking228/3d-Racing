using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : CarCameraComponent
{
    [SerializeField][Range(0f, 1f)] private float normalizedSpeedShake;
    [SerializeField] private float shakeAmount;

    private void Update()
    {
        if(car.LinearVelocityNormalized >= normalizedSpeedShake)
            transform.localPosition += Random.insideUnitSphere * shakeAmount * Time.deltaTime;
        

    }
}
