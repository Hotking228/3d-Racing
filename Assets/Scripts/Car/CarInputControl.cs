using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputControl : MonoBehaviour
{
    [SerializeField] private Car car;

    [SerializeField] private AnimationCurve brakeCurve;
    [SerializeField] private AnimationCurve steerCurve;

    [SerializeField][Range(0f, 1f)] private float autoBrakeStrength = 0.2f;


    private float wheelSpeed;
    private float verticalAxis;
    private float horizontalAxis;
    private float handBrake;


    private void Update()
    {
        wheelSpeed = car.WheelSpeed;

        UpdateAxis();

        UpdateThrott();
        UpdateSteer();


        

        UpdateAutoBrake();



        if (Input.GetKeyDown(KeyCode.E))
        {
            car.UpGear();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            car.DownGear();
        }

    }

    private void UpdateSteer()
    {
        car.SteerControl = steerCurve.Evaluate(wheelSpeed / (car.MaxSpeed*1.2f)) * horizontalAxis;
    }


    private void UpdateThrott()
    {

        if (Mathf.Sign(verticalAxis) == Mathf.Sign(wheelSpeed) || Mathf.Abs(wheelSpeed) < 0.5f)
        {
            car.ThrottControl = verticalAxis;
            car.BrakeControl = 0;
        }
        else
        {
            car.ThrottControl = 0;
            car.BrakeControl = brakeCurve.Evaluate(wheelSpeed / car.MaxSpeed);
        }

    }

    private void UpdateAxis()
    {
        verticalAxis = Input.GetAxis("Vertical");
        horizontalAxis = Input.GetAxis("Horizontal");
        handBrake = Input.GetAxis("Jump");
    }

    private void UpdateAutoBrake()
    {
        if (verticalAxis == 0)
        {
            car.BrakeControl = brakeCurve.Evaluate(wheelSpeed / car.MaxSpeed) * autoBrakeStrength;
        }
    }

    public void Stop()
    {
        Reset();

        car.BrakeControl = 1;
    }

    public void Reset()
    {
        verticalAxis = 0;
        horizontalAxis = 0;
        handBrake = 0;

        car.ThrottControl = 0;
        car.SteerControl = 0;
        car.BrakeControl = 0;

    }
}
