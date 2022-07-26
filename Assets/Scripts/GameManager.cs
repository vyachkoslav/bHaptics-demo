using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameStateHandler _handler;
    static GameStateHandler handler;

    private void Start()
    {
        if (_handler)
            handler = _handler;
    }
    public static void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public static void WinGame()
    {
        if (handler)
            handler.HandleWin();
        else
            SceneManager.LoadScene("Demo");
    }
    public static void LooseGame(string reason)
    {
        if (handler)
            handler.HandleLoose(reason);
        else
            SceneManager.LoadScene("Menu");
    }
    public static void CloseGame()
    {
        Application.Quit();
    }

    public static void ResetPlayerPosition()
    {
        if(handler)
            handler.PlayerReset();
    }

    public static void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
    }
    public static void SetTimeScaleWithSound(float scale)
    {
        SetTimeScale(scale);
        SetSoundPitch(scale);
    }
    public static void SetSoundPitch(float pitch)
    {
        List<AudioSource> audios = new(FindObjectsOfType<AudioSource>());
        audios.ForEach(x => x.pitch = pitch);
    }
}
