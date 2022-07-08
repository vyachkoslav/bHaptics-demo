using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                GameManager.LooseGame();
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
        }
    }
    
    public float Score { get; private set; }

    private void Start()
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