using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    /// <summary>
    /// Structure to deserialize from json
    /// </summary>
    [Serializable]
    struct PlayerScore
    {
        public PlayerScore(string _name, int _score)
        { 
            name = _name;
            score = _score;
        }
        public string name;
        public int score;
    }

    const string addScoreURL = "http://localhost/add?";
    const string getTopURL = "http://localhost/gettop?";
    const string leaderboardURL = "http://localhost/get";

    /// <summary>
    /// Text to display the result on
    /// </summary>
    public TMPro.TextMeshProUGUI statusText;

    /// <summary>
    /// Text to display position of the player on the leaderboard
    /// </summary>
    public TMPro.TextMeshProUGUI topPositionText;

    /// <summary>
    /// InputField which gets player name
    /// </summary>
    public TMPro.TMP_InputField nameInput;
    public Player player;

    void OnEnable()
    {
        StartCoroutine(GetScores());
        StartCoroutine(GetTop());
    }

    /// <summary>
    /// Gets leaderboard scores
    /// </summary>
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
            PlayerScore[] leaderboard = JsonHelper.FromJson<PlayerScore>(hs_get.downloadHandler.text);
            statusText.text = string.Empty;
            foreach (PlayerScore score in leaderboard)
            {
                string row = score.name + ": " + score.score;
                statusText.text += row + "\n";
            }
        }
    }

    /// <summary>
    /// Called by onClick event
    /// </summary>
    public void AddClicked(Button caller)
    {
        string name = nameInput.text.Trim();
        if (name != string.Empty && name[0] - '0' != 8155) // sometimes the empty string appears to be 8155 char - ?
        {
            caller.interactable = false;
            StartCoroutine(AddScore(nameInput.text, player.Score));
        }
    }

    /// <summary>
    /// Adds player score to the leaderboard
    /// </summary>
    IEnumerator AddScore(string name, int score) // todo safe request
    {
        string query = "name=" + name + "&" + "score=" + score;
        UnityWebRequest hs_get = UnityWebRequest.Get(addScoreURL + query);
        yield return hs_get.SendWebRequest();
        if (hs_get.error != null)
            print("There was an error getting the leaderboard: " + hs_get.error);
        else
            StartCoroutine(GetScores());
        
    }

    /// <summary>
    /// Gets position of the player in the leaderboard
    /// </summary>
    IEnumerator GetTop()
    {
        string query = "score=" + player.Score;
        UnityWebRequest hs_get = UnityWebRequest.Get(getTopURL + query);
        yield return hs_get.SendWebRequest();
        if (hs_get.error != null)
            print("There was an error getting the leaderboard: " + hs_get.error);
        else
            topPositionText.text = hs_get.downloadHandler.text;
    }
}
