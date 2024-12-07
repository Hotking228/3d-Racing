using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[Serializable]
public class WheelAxel 
{
    [SerializeField] private WheelCollider leftWheelCollider;
    [SerializeField] private WheelCollider rightWheelCollider;


    [SerializeField] private Transform leftWheelMesh;
    [SerializeField] private Transform rightWheelMesh;

    [SerializeField] private bool isMotor;
    [SerializeField] private bool isSteer;

    public bool IsMotor=>isMotor;
    public bool Steer =>isSteer;

    [SerializeField] private float wheelWidth;
    [SerializeField] private float antiRollForce;


    [SerializeField] private float additionalWheelDownForce;
    
    [SerializeField] private float baseForwardStiffness = 1.5f;
    [SerializeField] private float stabilityForwardFactor = 1.0f;


    [SerializeField] private float baseSideWaysStiffness = 1.5f;
    [SerializeField] private float stabilitySideWaysFactor = 1.0f;

    private WheelHit leftWheelHit;
    private WheelHit rightWheelHit;


    #region Public API
    public void Update()
    {
        UpdateWheelHits();
        ApplyAntiRoll();
        ApplyDownForce();
        CorrectStiffness();
        SyncMeshTransform();
    }

    private void UpdateWheelHits()
    {
        leftWheelCollider.GetGroundHit(out leftWheelHit);
        rightWheelCollider.GetGroundHit(out rightWheelHit);
    }

    private void CorrectStiffness()
    {
        WheelFrictionCurve leftForward = leftWheelCollider.forwardFriction;
        WheelFrictionCurve rightForward = rightWheelCollider.forwardFriction;

        WheelFrictionCurve leftSideWays = leftWheelCollider.sidewaysFriction;
        WheelFrictionCurve rightSideWays = rightWheelCollider.sidewaysFriction;

        leftForward.stiffness = baseForwardStiffness + MathF.Abs(leftWheelHit.forwardSlip) * stabilityForwardFactor;
        rightForward.stiffness = baseForwardStiffness + MathF.Abs(rightWheelHit.forwardSlip) * stabilityForwardFactor;



        leftSideWays.stiffness = baseSideWaysStiffness + MathF.Abs(leftWheelHit.sidewaysSlip) * stabilitySideWaysFactor;
        rightSideWays.stiffness = baseSideWaysStiffness + MathF.Abs(rightWheelHit.sidewaysSlip) * stabilitySideWaysFactor;


        leftWheelCollider.forwardFriction = leftForward;
        rightWheelCollider.forwardFriction = rightForward;

        leftWheelCollider.sidewaysFriction = leftSideWays;
        rightWheelCollider.sidewaysFriction = rightSideWays;

    }

    private void ApplyDownForce()
    {
        if (leftWheelCollider.isGrounded)
        {
            leftWheelCollider.attachedRigidbody.AddForceAtPosition(leftWheelHit.normal * (- additionalWheelDownForce)* leftWheelCollider.attachedRigidbody.velocity.magnitude, leftWheelCollider.transform.position);
        }
    }

    private void ApplyAntiRoll()
    {
        float travelL = 1;
        float travelR = 1;

        if (leftWheelCollider.isGrounded)
        {
            travelL = (-leftWheelCollider.transform.InverseTransformPoint( leftWheelHit.point).y - leftWheelCollider.radius) / leftWheelCollider.suspensionDistance;
        }
        if (rightWheelCollider.isGrounded)
        {
            travelR = (-rightWheelCollider.transform.InverseTransformPoint(rightWheelHit.point).y - rightWheelCollider.radius) / rightWheelCollider.suspensionDistance;
        }

        float forceDir = (travelL - travelR);
        if (leftWheelCollider.isGrounded)
        {
            leftWheelCollider.attachedRigidbody.AddForceAtPosition(leftWheelCollider.transform.up * (-forceDir) * antiRollForce, leftWheelCollider.transform.position);
        }
        if (rightWheelCollider.isGrounded)
        {
            rightWheelCollider.attachedRigidbody.AddForceAtPosition(rightWheelCollider.transform.up * (forceDir) * antiRollForce, rightWheelCollider.transform.position);
        }
    }

    public void ApplySteerAngle(float steerAngle, float wheelBaseLength)
    {
        if (!isSteer) return;

        float radius = Mathf.Abs(wheelBaseLength * Mathf.Tan( Mathf.Deg2Rad * ( 90 - Mathf.Abs( steerAngle ))));
        float angleSign = Mathf.Sign(steerAngle);

        if (steerAngle > 0)
        {
            leftWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius + wheelWidth * 0.5f)) * angleSign;
            rightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius - wheelWidth * 0.5f)) * angleSign;
        }
        else if (steerAngle < 0)
        {
            leftWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius - wheelWidth * 0.5f)) * angleSign;
            rightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius + wheelWidth * 0.5f)) * angleSign;
        }
        else
        {


            leftWheelCollider.steerAngle = 0;
            rightWheelCollider.steerAngle = 0;
        }
        
        

    }

    public void ApplyMotorTorque(float motorTorque)
    {
        if (!isMotor) return;


        leftWheelCollider.motorTorque = motorTorque;
        rightWheelCollider.motorTorque = motorTorque;
    }


    public void ApplyBrakeTorque(float brakeTorque)
    {
        leftWheelCollider.brakeTorque = brakeTorque;
        rightWheelCollider.brakeTorque = brakeTorque;
    }





    public float GetAverageRPM()
    {
        return (leftWheelCollider.rpm + rightWheelCollider.rpm) / 2;
    }

    public float GetRadius()
    {
        return leftWheelCollider.radius;
    }

    #endregion

    #region Private
    private void SyncMeshTransform()
    {
        UpdateWheelTransform(leftWheelCollider, leftWheelMesh);
        UpdateWheelTransform(rightWheelCollider, rightWheelMesh);
    }


    private void UpdateWheelTransform(WheelCollider wheelCollider, Transform wheelTransform )
    {
        Vector3 pos;
        Quaternion rot;

        wheelCollider.GetWorldPose(out pos, out rot);
        rot = Quaternion.Euler(rot.eulerAngles.x, rot.eulerAngles.y, rot.eulerAngles.z);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }
    #endregion
}
