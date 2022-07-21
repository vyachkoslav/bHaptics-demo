using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleEventHandler : MonoBehaviour
{
    [SerializeField] int scorePerObstacle;
    [SerializeField] Player player;
    [SerializeField] PlayerDataOutput output;

    public static ObstacleEventHandler Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }

    public void HandlePlayerHit()
    {
        if (player)
        {
            player.ResetMultiplier();
            player.Lives--;

            if(output)
                output.UpdateOutput();
        }
    }
    public void AddPlayerScore(int amount)
    {
        player.AddScore(amount);
        if (output)
            output.UpdateOutput();
    }
    public void AddPlayerCombo(int amount)
    {
        player.Multiplier += amount;
        if (output)
            output.UpdateOutput();
    }
    public void HandleScore(Transform other)
    {
        if (player)
        {
            AddPlayerScore(scorePerObstacle);
            AddPlayerCombo(1);

            if (other.TryGetComponent(out AudioSource source))
            {
                source.Play();
            }
        }
    }
}
