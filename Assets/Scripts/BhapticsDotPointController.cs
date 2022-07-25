using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bhaptics.Tact;
using Bhaptics.Tact.Unity;


public class BhapticsDotPointController : MonoBehaviour
{
    public HapticClipPositionType clipPositionType;

    [HideInInspector] public List<DotPoint> dotPointList;

    private string key = System.Guid.NewGuid().ToString();

    private int duration = 100;


    void Awake()
    {
        dotPointList = new List<DotPoint>();
    }

    void Update()
    {
        BhapticsVirtualManager.Submit(key, BhapticsUtils.ToPositionType(clipPositionType), dotPointList, duration);
    }

    public void Toggle(DotPoint dot)
    {
        if (IsActive(dot))
        {
            RemoveAtList(dot);
        }
        else
        {
            AddToList(dot);
        }
    }
    public void TurnOn(DotPoint dot)
    {
        if (!IsActive(dot))
            AddToList(dot);
    }
    public void TurnOff(DotPoint dot)
    {
        if (IsActive(dot))
            RemoveAtList(dot);
    }
    public bool IsActive(DotPoint dot)
    {
        return dotPointList.Contains(dot);
    }

    private bool AddToList(DotPoint dot)
    {
        if (dotPointList.Contains(dot))
        {
            return false;
        }
        dotPointList.Add(dot);
        return true;
    }

    private bool RemoveAtList(DotPoint dot)
    {
        if (dotPointList.Contains(dot))
        {
            dotPointList.Remove(dot);
            return true;
        }
        return false;
    }
}
