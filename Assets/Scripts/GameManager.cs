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
    public static void LooseGame()
    {
        if (handler)
            handler.HandleLoose();
        else
            SceneManager.LoadScene("Menu");
    }
    public static void CloseGame()
    {
        Application.Quit();
    }
}
