using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataOutput : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] TMPro.TextMeshProUGUI scoreOutput;
    [SerializeField] TMPro.TextMeshProUGUI comboOutput;

    public void UpdateOutput()
    {
        scoreOutput.text = player.Score.ToString();
        comboOutput.text = player.Multiplier.ToString();
    }
}
