using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bhaptics.Tact;
using Bhaptics.Tact.Unity;

/// <summary>
/// Point which activates haptics on collision
/// </summary>
public class BhapticsDotPoint : MonoBehaviour
{
    public int motorIndex = 0;
    public int motorIntensity = 100;


    private BhapticsDotPointController controller;
    private DotPoint dotPoint;
    [SerializeField] ParticleSystem particles;
    [SerializeField] hapticSoundEffect hapticSound;
    [SerializeField] Material defaultMat;
    [SerializeField] Material activeMat;

    void Awake()
    {
        dotPoint = new DotPoint(motorIndex, motorIntensity);
        controller = GetComponentInParent<BhapticsDotPointController>();
        if(hapticSound == null)
            TryGetComponent(out hapticSound);
    }

    public void Toggle()
    {
        if (controller)
        {
            controller.Toggle(dotPoint);
            if (controller.IsActive(dotPoint))
                GetComponent<MeshRenderer>().material = activeMat;
            else
                GetComponent<MeshRenderer>().material = defaultMat;
        }
    }
    public void TurnOn()
    {
        if (controller)
            controller.TurnOn(dotPoint);
        GetComponent<MeshRenderer>().material = activeMat;
    }
    public void TurnOff()
    {
        if (controller)
            controller.TurnOff(dotPoint);
        GetComponent<MeshRenderer>().material = defaultMat;
    }
    public void PlayEffect()
    {
        particles.Play();
        if (!hapticSound.isPlaying)
            hapticSound.Play();
    }
    protected bool HasComponent<Component>(GameObject obj) where Component : UnityEngine.Component
    {
        return obj.TryGetComponent<Component>(out var _) || obj.GetComponentInParent<Component>();

    }
    void OnTriggerEnter(Collider other)
    {
        if (HasComponent<HapticSender>(other.gameObject))
        {
            PlayEffect();
            TurnOn();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (HasComponent<HapticSender>(other.gameObject))
        {
            TurnOff();
        }
    }
    private void OnDisable()
    {
        TurnOff();
    }
}