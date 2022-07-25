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
    [SerializeField] Vector3 defaultPosition;

    [SerializeField] Transform testObject;
    Vector3 defaultObjectPosition;
    Quaternion defaultObjectRotation;

    private void Awake()
    {
        defaultObjectPosition = testObject.position;
        defaultObjectRotation = testObject.rotation;
    }
    public void ResetPlayerPos()
    {
        playerOrigin.position += defaultPosition - playerTransform.position;
    }
    public void ResetObject()
    {
        testObject.position = defaultObjectPosition;
        testObject.rotation = defaultObjectRotation;
    }
    public void RotateTestObjectBy180()
    {
        testObject.Rotate(new Vector3(0, 180, 0));
    }
}
