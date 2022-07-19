using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public static void WinGame()
    {
        SceneManager.LoadScene("Demo");
    }
    public static void LooseGame()
    {
        SceneManager.LoadScene("Menu");
    }
    public static void CloseGame()
    {
        Application.Quit();
    }
}
