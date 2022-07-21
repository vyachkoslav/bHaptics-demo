using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    const int k_playerLayer = 8;
    const string k_scoreColliderTag = "ScoreCollider";

    [SerializeField] int scorePerObstacle;
    bool triggered;
    void OnEnable()
    {
        triggered = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        if(other.gameObject.GetComponentInParent<Player>())
        {
            ObstacleEventHandler.Instance.HandlePlayerHit();
            triggered = true;
        }
        else if(other.CompareTag(k_scoreColliderTag))
        {
            ObstacleEventHandler.Instance.HandleScore(other.transform);
            triggered = true;
        }
    }
}
