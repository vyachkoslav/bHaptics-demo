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
    [SerializeField] AudioSource audioSource;
    [SerializeField] Material defaultMat;
    [SerializeField] Material activeMat;

    bool _active;
    protected bool active { 
        get { return _active; }
        set
        {
            _active = value;
            if (value)
                GetComponent<MeshRenderer>().material = activeMat;
            else
                GetComponent<MeshRenderer>().material = defaultMat;
        }
    }

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
        if (!audioSource.isPlaying)
            audioSource.Play();
    }
    protected bool IsPlayer(GameObject obj)
    {
        return obj.TryGetComponent<Player>(out var _) || obj.GetComponentInParent<Player>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (!active && !IsPlayer(other.gameObject))
        {
            active = true;
            PlayEffect();
            Toggle();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (active && !IsPlayer(other.gameObject))
        {
            active = false;
            Toggle();
        }
    }
}