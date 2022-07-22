using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Gives score to the player on hit
/// </summary>
public class ScoreGiver : MonoBehaviour
{
    [SerializeField] int scoreAmount;
    [Range(0, 1)][SerializeField] float chanceToAppear;

    /// <summary>
    /// Child object with renderer and collider. Shouldn't be the object with ScoreGiver.
    /// </summary>
    [SerializeField] GameObject colliderObject;

    private void OnEnable()
    {
        colliderObject.SetActive(chanceToAppear >= Random.Range(0f, 1f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Player>())
        {
            ObstacleEventHandler.Instance.AddPlayerScore(scoreAmount);
            colliderObject.SetActive(false);
        }
    }
}
