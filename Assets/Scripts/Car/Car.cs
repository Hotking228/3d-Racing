using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour
{
    private CarChassis chassis;
    public Rigidbody Rigidbody => chassis==null?GetComponent<CarChassis>().Rigidbody:chassis.Rigidbody;

    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float maxBrakeTorque;

    [SerializeField] private float linearVelocity;
    public float LinearVelocity => chassis.LinearVelocity;
    public float LinearVelocityNormalized => chassis.LinearVelocity / MaxSpeed;
    public float WheelSpeed => chassis.GetWheelSpeed();
    public float MaxSpeed => maxSpeed;

    [Header("Engine")]
    [SerializeField] private AnimationCurve engineTorqueCurve;
    [SerializeField] private float engineMaxTorque;
    [SerializeField] private float engineTorque;
    [SerializeField] private float engineRpm;
    public float EngineRpmNormalized => engineRpm/engineMaxRpm;
    [SerializeField] private float engineMinRpm;
    [SerializeField] private float engineMaxRpm;



    [SerializeField] private float maxSpeed;


    public float ThrottControl;
    public float SteerControl;
    public float BrakeControl;


    [Header("GearBox")]
    [SerializeField] private float[] gears;
    [SerializeField] private float finalDriveRatio;
    [SerializeField] private float selectedGear;
    [SerializeField] private float rearGear;
    [SerializeField] private int selectedGearIndex;
    public int SelectedGearIndex=>selectedGearIndex;
    [SerializeField]private float upShiftEngineRpm;
    [SerializeField] private float downShiftEngineRpm;
    [SerializeField] private AudioSource shiftGearSound;
    private void Start()
    {
        chassis = GetComponent<CarChassis>();
    }



    private void Update()
    {
        linearVelocity = LinearVelocity;
        UpdateEngineTorque();
        AutoGearShift();
        
        if (LinearVelocity >= maxSpeed)
            engineTorque = 0;

        chassis.motorTorque = ThrottControl * engineTorque;
        chassis.brakeTorque = BrakeControl * maxBrakeTorque;
        chassis.steerAngle = SteerControl * maxSteerAngle;

    }


    #region GearBox


    private void AutoGearShift()
    {
        if (selectedGear < 0) return;

        if (engineRpm >= upShiftEngineRpm)
        {
            UpGear();
            shiftGearSound.Play();
        }
        if (engineRpm <= downShiftEngineRpm)
        {
            DownGear();
            shiftGearSound.Play();
        }
        
        
        selectedGearIndex = Mathf.Clamp(selectedGearIndex, 0, gears.Length - 1);
    }


    public void UpGear()
    {
        ShiftGear(selectedGearIndex + 1);
    }

    public void DownGear()
    {
        ShiftGear(selectedGearIndex - 1);
    }

    public void ShiftToReverseGear()
    {
        selectedGear = rearGear;
        selectedGearIndex = -1;
    }


    public void ShiftToNeutral()
    {
        selectedGear = 0;
        selectedGearIndex = -1;
    }    

    private void ShiftGear(int gearIndex)
    {
        gearIndex = Mathf.Clamp(gearIndex, 0, gears.Length - 1);
        selectedGear = gears[gearIndex];
        selectedGearIndex = gearIndex;
    }






    #endregion
    private void UpdateEngineTorque()
    {
        engineRpm = engineMinRpm + Mathf.Abs(chassis.GetAverageRPM() * selectedGear * finalDriveRatio);
        engineRpm = Mathf.Clamp(engineRpm, engineMinRpm, engineMaxRpm);

        engineTorque = engineTorqueCurve.Evaluate(engineRpm / engineMaxRpm) * engineMaxTorque * finalDriveRatio * Mathf.Sign(selectedGear);
    }
    public void Respawn(Vector3 position, quaternion rotation)
    {
        Reset();
        transform.position = position;
        transform.rotation = rotation;
    }

    public void Reset()
    {
        
        chassis.Reset();

        //chassis.motorTorque = 0;
        //chassis.brakeTorque = 0;
        //chassis.steerAngle = 0;
        //ThrottControl = 0;
        //BrakeControl = 0;
        //SteerControl = 0;
    }
}
