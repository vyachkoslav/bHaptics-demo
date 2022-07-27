using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Copy : MonoBehaviour
{
    [SerializeField] Transform copied;
    Vector3 positionOffset;
    public void SetCopied(Transform obj)
    {
        copied = obj;
        positionOffset = transform.position - copied.position;
        positionOffset = Vector3.Scale(positionOffset, new Vector3(1, 0, 1));
    }
    private void Awake()
    {
        if (copied)
            SetCopied(copied);
    }
    void Update()
    {
        if (copied)
        {
            transform.SetPositionAndRotation(copied.position + positionOffset, copied.rotation);
            transform.localScale = copied.localScale;
        }
    }
}
