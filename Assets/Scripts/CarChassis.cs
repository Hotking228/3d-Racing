using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarChassis : MonoBehaviour
{
    [SerializeField] private WheelAxel[] wheelAxles;
    [SerializeField] private float wheelbaselength;
     public float motorTorque;
     public float brakeTorque;
     public float steerAngle;
    [SerializeField] private Transform centerOfMass;

    private new Rigidbody rigidbody;

    public float LinearVelocity => rigidbody.velocity.magnitude * 3.6f;
    



    [Header("Down Drag")]
    [SerializeField] private float downDragMin;
    [SerializeField] private float downDragMax;
    [SerializeField] private float downDragFactor;

    [Header("Angular Drag")]
    [SerializeField] private float angularDragMin;
    [SerializeField] private float angularDragMax;
    [SerializeField] private float angularDragFactor;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();


        if (centerOfMass != null)
            rigidbody.centerOfMass = centerOfMass.localPosition;
    }

    private void FixedUpdate()
    {
        UpdateDownForce();
        UpdateAngularDrag();
        UpdateWheelAxles();
    }


    public float GetAverageRPM()
    {
        float sum = 0;
        for (int i = 0; i < wheelAxles.Length; i++)
        {
            sum += wheelAxles[i].GetAverageRPM();
        }

        return sum / wheelAxles.Length;
    }

    public float GetWheelSpeed()
    {
        return GetAverageRPM() * wheelAxles[0].GetRadius() * 2 * 0.1885f;
    }


    private void UpdateDownForce()
    {
        float downForce = Mathf.Clamp(downDragFactor * LinearVelocity, downDragMin, downDragMax);
        rigidbody.AddForce(-transform.up * downForce);
    }

    private void UpdateAngularDrag()
    {
        rigidbody.angularDrag = Mathf.Clamp(angularDragFactor * rigidbody.velocity.magnitude, angularDragMin, angularDragMax);
        
    }

    private void UpdateWheelAxles()
    {
        int amountMotorWheel = 0;


        for (int i = 0; i < wheelAxles.Length; i++)
        {
            if (wheelAxles[i].IsMotor)
                amountMotorWheel += 2;
        }

        for (int i = 0; i < wheelAxles.Length; i++)
        {
            wheelAxles[i].Update();


            wheelAxles[i].ApplyMotorTorque(motorTorque / amountMotorWheel);
            wheelAxles[i].ApplySteerAngle(steerAngle, wheelbaselength);
            wheelAxles[i].ApplyBrakeTorque(brakeTorque);
        }
    }
}
