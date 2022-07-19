using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bhaptics.Tact;
using Bhaptics.Tact.Unity;


public class BhapticsDotPoint : MonoBehaviour
{
    public int motorIndex = 0;
    public int motorIntensity = 100;


    private BhapticsDotPointController controller;
    private DotPoint dotPoint;
    [SerializeField] ParticleSystem particles;



    void Awake()
    {
        dotPoint = new DotPoint(motorIndex, motorIntensity);
        controller = GetComponentInParent<BhapticsDotPointController>();
    }

    public void Toggle()
    {
        if (controller == null)
        {
            return;
        }

        controller.Toggle(dotPoint);
    }
    public void PlayEffect()
    {
        particles.Play();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponentInParent<Unity.XR.CoreUtils.XROrigin>())
        {
            PlayEffect();
            Toggle();
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (!other.GetComponentInParent<Unity.XR.CoreUtils.XROrigin>())
        {
            PlayEffect();
            Toggle();
        }
    }
}