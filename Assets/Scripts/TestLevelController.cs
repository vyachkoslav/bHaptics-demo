using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI controls handler
/// </summary>
public class TestLevelController : MonoBehaviour
{
    [SerializeField] Transform playerOrigin;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform testObject;
    [SerializeField] Vector3 defaultPosition;
    public void ResetPlayerPos()
    {
        playerOrigin.position += defaultPosition - playerTransform.position;
    }
    public void RotateTestObjectBy180()
    {
        testObject.Rotate(new Vector3(0, 180, 0));
    }
}
