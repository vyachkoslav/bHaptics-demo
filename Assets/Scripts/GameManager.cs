using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static void WinGame()
    {
        SceneManager.LoadScene("Demo");
    }
    public static void LooseGame()
    {
        SceneManager.LoadScene("Demo");
    }
    public static void CloseGame()
    {
        Application.Quit();
    }
}