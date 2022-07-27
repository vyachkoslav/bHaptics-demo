using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Saves player data between scenes
/// </summary>
[CreateAssetMenu(fileName = "Player data", menuName = "ScriptableObjects/Player Data", order = 1)]
public class PlayerData : ScriptableObject
{
    public Vector3 originPosition;
    public bool isHologramOn;
    public void SetHologramActive(bool value)
    {
        isHologramOn = value;
    }
}
