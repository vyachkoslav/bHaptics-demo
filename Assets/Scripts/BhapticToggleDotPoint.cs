using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BhapticToggleDotPoint : BhapticsDotPoint
{
    void OnTriggerEnter(Collider other)
    {
        if (!IsPlayer(other.gameObject))
        {
            active = !active;
            PlayEffect();
            Toggle();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
}
