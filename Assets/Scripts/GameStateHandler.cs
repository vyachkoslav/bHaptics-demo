using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandler : MonoBehaviour
{
    /// <summary>
    /// Menu to appear when the game ends
    /// </summary>
    [SerializeField] GameObject endMenu;
    /// <summary>
    /// Title of the end menu
    /// </summary>
    [SerializeField] TMPro.TextMeshProUGUI menuTitle;
    /// <summary>
    /// Object that creates obstacles
    /// </summary>
    [SerializeField] ObjectCreator creator;
    /// <summary>
    /// Scripts to active when the game ends
    /// </summary>
    [SerializeField] List<MonoBehaviour> scriptsToActivate;
    /// <summary>
    /// Objects to deactivate when the game ends
    /// </summary>
    [SerializeField] List<GameObject> objectsToHide;

    void Start()
    {
        endMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void HandleLoose()
    {
        if(creator)
            creator.DestroyAllCreatedObjects();
        endMenu.SetActive(true);
        menuTitle.text = "Game Over";

        scriptsToActivate.ForEach(x => x.enabled = true);
        objectsToHide.ForEach(x => x.SetActive(false));
        var dots = FindObjectsOfType<BhapticsDotPoint>();
        foreach(var dot in dots)
            dot.enabled = false;
        Time.timeScale = 0f;
    }
    public void HandleWin()
    {
        HandleLoose();
        menuTitle.text = "You won!";
    }
}
