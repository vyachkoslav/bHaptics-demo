using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreGiver : MonoBehaviour
{
    [SerializeField] int scoreAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Player>())
        {
            ObstacleEventHandler.Instance.AddPlayerScore(scoreAmount);
            Destroy(gameObject);
        }
    }
}
