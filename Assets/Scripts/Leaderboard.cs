using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    string addScoreURL = "http://localhost/add?";
    string getTopURL = "http://localhost/gettop?";
    string leaderboardURL = "http://localhost/getstr";

    //Text to display the result on
    public TMPro.TextMeshProUGUI statusText;
    public TMPro.TextMeshProUGUI topPositionText;
    public TMPro.TMP_InputField nameInput;
    public Player player;

    void OnEnable()
    {
        StartCoroutine(GetScores());
        StartCoroutine(GetTop());
    }
    IEnumerator GetScores()
    {
        UnityWebRequest hs_get = UnityWebRequest.Get(leaderboardURL);
        yield return hs_get.SendWebRequest();

        if (hs_get.error != null)
        {
            print("There was an error getting the leaderboard: " + hs_get.error);
        }
        else
        {
            statusText.text = hs_get.downloadHandler.text;
        }
    }
    public void AddClicked(Button caller)
    {
        string name = nameInput.text;
        if (name[0] - '0' != 8155)
        {
            caller.interactable = false;
            StartCoroutine(AddScore(nameInput.text, player.Score));
        }
    }
    IEnumerator AddScore(string name, int score) // todo safe request
    {
        string query = "name=" + name + "&" + "score=" + score;
        UnityWebRequest hs_get = UnityWebRequest.Get(addScoreURL + query);
        yield return hs_get.SendWebRequest();
        StartCoroutine(GetScores());
    }
    IEnumerator GetTop()
    {
        string query = "score=" + player.Score;
        UnityWebRequest hs_get = UnityWebRequest.Get(getTopURL + query);
        yield return hs_get.SendWebRequest();
        if (hs_get.error != null)
        {
            print("There was an error getting the leaderboard: " + hs_get.error);
        }
        else
        {
            topPositionText.text = hs_get.downloadHandler.text;
        }
    }
}
