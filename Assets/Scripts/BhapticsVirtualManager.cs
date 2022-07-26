using Bhaptics.Tact;
using Bhaptics.Tact.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Stores haptics data when bHaptics isn't active
/// </summary>
public class BhapticsVirtualManager
{
    static Dictionary<PositionType, List<DotPoint>> hapticDots = new();

    static IHaptic haptic => BhapticsManager.GetHaptic();
    public static void Submit(string key, PositionType position, List<DotPoint> points, int durationMillis)
    {
        if (haptic != null)
        {
            haptic.Submit(key, position, points, durationMillis);
        }
        hapticDots[position] = points;
    }
    public static int[] GetCurrentFeedback(PositionType pos)
    {
        if (haptic != null)
        {
            var feedback = haptic.GetCurrentFeedback(pos);
            if (new List<int>(feedback).Exists(x => { return x > 0; }))
                return feedback;
        }


        if (!hapticDots.ContainsKey(pos))
            return new int[0];
        List<int> result = new();
        List<DotPoint> dots = hapticDots[pos];
        for(int i = 0; i < dots.Count; ++i)
        {
            while (dots[i].Index >= result.Count)
                result.Add(0);
            result[dots[i].Index] = 1;
        }
        return result.ToArray();
    }
}
