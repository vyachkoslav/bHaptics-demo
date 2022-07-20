using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataOutput : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] TMPro.TextMeshProUGUI scoreOutput;
    [SerializeField] TMPro.TextMeshProUGUI comboOutput;
    [SerializeField] TMPro.TextMeshProUGUI maxComboOutput;

    private void OnEnable()
    {
        UpdateOutput();
    }
    public void UpdateOutput()
    {
        if(scoreOutput)
            scoreOutput.text = player.Score.ToString();
        if(comboOutput)
            comboOutput.text = player.Multiplier.ToString();
        if (maxComboOutput)
            maxComboOutput.text = player.MaxMultiplierAchieved.ToString();
    }
}
