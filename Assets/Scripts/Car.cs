using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour
{
    private CarChassis chassis;
    [SerializeField] private float maxMotorTorque;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float maxBrakeTorque;

    public float LinearVelocity => chassis.LinearVelocity;
    public float WheelSpeed => chassis.GetWheelSpeed();
    public float MaxSpeed => maxSpeed;

    [SerializeField] private AnimationCurve engineTorqueCurve;

    [SerializeField] private float maxSpeed;
    public float ThrottControl;
    public float SteerControl;
    public float BrakeControl;

    
    private void Start()
    {
        chassis = GetComponent<CarChassis>();
    }



    private void FixedUpdate()
    {

        float engineTorque = engineTorqueCurve.Evaluate(LinearVelocity);

        if (LinearVelocity >= maxSpeed)
            engineTorque = 0;

        chassis.motorTorque = ThrottControl * engineTorque;
        chassis.brakeTorque = BrakeControl * maxBrakeTorque;
        chassis.steerAngle = SteerControl * maxSteerAngle;

    }

}
