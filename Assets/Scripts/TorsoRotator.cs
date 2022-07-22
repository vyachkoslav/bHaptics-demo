using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Imitates torso rotation of the player
/// </summary>
public class TorsoRotator : MonoBehaviour
{
    [SerializeField] Transform torso;
    [SerializeField] Transform leftHand;
    [SerializeField] Transform rightHand;
    [SerializeField] Transform head;
    [SerializeField] float maxHeadHeight; // from y = 0

    Vector3 offset;

    void Start()
    {
        if (head)
            offset = torso.position - head.position;
    }

    private void Update()
    {
        if (leftHand && rightHand) // directs body between hands
        {
            Vector3 filter = Vector3.forward + Vector3.right;
            Vector3 dirToLeft = (leftHand.position - rightHand.position).normalized;
            dirToLeft = Vector3.Scale(dirToLeft, filter);
            dirToLeft = Quaternion.AngleAxis(90, Vector3.up) * dirToLeft;

            Quaternion lookRot = Quaternion.LookRotation(dirToLeft);
            torso.rotation = lookRot;
        }
        if(head) 
        {
            torso.position = head.position + offset; // follow head

            // handles player sitting
            float height = head.transform.position.y;
            float ratio = height / maxHeadHeight;
            ratio = Mathf.Clamp01(ratio);

            float eulerAmount = Mathf.Abs(90f * ratio - 90f);
            torso.eulerAngles = new Vector3(eulerAmount, torso.eulerAngles.y, torso.eulerAngles.z);
        }
    }
}
