using Bhaptics.Tact.Unity;
using UnityEngine;


public class Bhaptics3DVisualizer : MonoBehaviour
{
    private Visual3DFeedback[] visualFeedback;


    void Awake()
    {
        visualFeedback = GetComponentsInChildren<Visual3DFeedback>();
    }
    
    void Update()
    {
        if (!BhapticsManager.Init)
        {
            return;
        }

        var haptic = BhapticsManager.GetHaptic();

        if (haptic == null)
        {
            return;
        }

        foreach (var vis in visualFeedback)
        {
            var feedback = haptic.GetCurrentFeedback(BhapticsUtils.ToPositionType(vis.devicePos));
    
            vis.UpdateFeedback(feedback);
        }
    }
}