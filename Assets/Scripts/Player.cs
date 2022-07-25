using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Player lives and score data
/// </summary>
public class Player : MonoBehaviour
{
    [SerializeField] int maxLives;
    int lives;
    public int Lives
    {
        get { return lives; }
        set
        {
            lives = Mathf.Clamp(value, 0, maxLives);
            if (lives == 0)
                GameManager.LooseGame("Game over");
        }
    }

    [SerializeField] int maxMultiplier;
    int multiplier;
    public int Multiplier
    {
        get { return multiplier; }
        set
        {
            multiplier = Mathf.Clamp(value, 1, maxMultiplier);

            if (multiplier > MaxMultiplierAchieved)
                MaxMultiplierAchieved = value;
        }
    }
    public int MaxMultiplierAchieved{ get; private set; }
    
    public int Score { get; private set; }

    private void Awake()
    {
        ResetMultiplier();
        Lives = maxLives;
    }

    public void AddScore(int amount)
    {
        Score += Multiplier * amount;
    }
    public void ResetScore()
    {
        Score = 0;
    }
    public void ResetMultiplier()
    {
        Multiplier = 1;
    }
}
