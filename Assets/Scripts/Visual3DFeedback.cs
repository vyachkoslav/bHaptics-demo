using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Bhaptics.Tact.Unity;
using Bhaptics.Tact;

public class Visual3DFeedback : MonoBehaviour
{
    public HapticClipPositionType devicePos = HapticClipPositionType.Head;
    public Gradient motorFeedbackGradient;

    private BhapticsDotPoint[] motors;

    void Start()
    {
        motors = GetComponentsInChildren<BhapticsDotPoint>();

        UpdateFeedback(new HapticFeedback(BhapticsUtils.ToPositionType(devicePos), new byte[motors.Length]));
    }

    public void UpdateFeedback(HapticFeedback feedback)
    {
        UpdateFeedback(System.Array.ConvertAll(feedback.Values, System.Convert.ToInt32));
    }

    public void UpdateFeedback(int[] feedbackValues)
    {
        if (motors == null)
            return;
        foreach(var motor in motors)
        {
            if (motor == null)
                return;

            float intensity = feedbackValues[motor.motorIndex];
            if (intensity > 0f)
                motor.PlayEffect();
        }
    }
}