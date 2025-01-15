using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(AudioSource))]
public class EngineSound : MonoBehaviour
{

    [SerializeField] private AudioSource windSound;
    

    [SerializeField] private Car car;
    private AudioSource engineAudioSource;
    [SerializeField] private float pitchModifier;
    [SerializeField] private float volumeModifier;
    [SerializeField] private float rpmModifier;


    [SerializeField] private float basePitch = 1.0f;
    [SerializeField] private float baseVolume = 0.4f;

    private void Start()
    {
        engineAudioSource = GetComponent<AudioSource>();


    }


    private void Update()
    {
        if (car.LinearVelocityNormalized >= 0.5f)
        {
            if(!windSound.isPlaying)
                windSound.Play();
        }
        else
        {
            windSound.Stop();
        }
        engineAudioSource.pitch = basePitch + pitchModifier * ((car.EngineRpmNormalized) * rpmModifier);
        engineAudioSource.volume = baseVolume + volumeModifier * (car.EngineRpmNormalized);
    }
}
