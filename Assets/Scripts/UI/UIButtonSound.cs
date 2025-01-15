using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class UIButtonSound : MonoBehaviour
{


    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip select; 
    private new AudioSource audio;

    private UIButton[] uiButtons;

    private void Start()
    {
        audio = GetComponent<AudioSource>();

        uiButtons = GetComponentsInChildren<UIButton>(true);


        for (int i = 0; i < uiButtons.Length; i++)
        {
            uiButtons[i].PointerEnter += OnPointerEnter;
            uiButtons[i].PointerClick += OnPointerClicked;
        }

    }
    private void OnDestroy()
    {

        for (int i = 0; i < uiButtons.Length; i++)
        {
            uiButtons[i].PointerEnter -= OnPointerEnter;
            uiButtons[i].PointerClick -= OnPointerClicked;
        }
    }

    private void OnPointerClicked(UIButton arg0)
    {
        audio.PlayOneShot(click);
    }
    private void OnPointerEnter(UIButton arg0)
    {
        audio.PlayOneShot(select);
    }
}
