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
            PlayEffect();
            Toggle();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // hides ontriggerexit of parent
    }
}
