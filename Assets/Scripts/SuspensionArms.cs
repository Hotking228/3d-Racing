using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspensionArms : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float factor;

    private float baseOffset;


    private void Start()
    {
        baseOffset = target.position.y;
    }


    private void Update()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, (target.localPosition.y + baseOffset) * factor);
    }
}
