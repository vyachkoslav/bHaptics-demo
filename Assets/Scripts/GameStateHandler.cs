using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandler : MonoBehaviour
{
    [SerializeField] GameObject endMenu;
    [SerializeField] TMPro.TextMeshProUGUI menuTitle;
    [SerializeField] ObjectCreator creator;
    [SerializeField] List<MonoBehaviour> scriptsToActivate;
    [SerializeField] List<GameObject> objectsToHide;

    void Start()
    {
        endMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void HandleLoose()
    {
        Time.timeScale = 0f;
        if(creator)
            creator.DestroyAllCreatedObjects();
        endMenu.SetActive(true);
        menuTitle.text = "Game Over";

        scriptsToActivate.ForEach(x => x.enabled = true);
        objectsToHide.ForEach(x => x.SetActive(false));
    }
    public void HandleWin()
    {
        HandleLoose();
        menuTitle.text = "You won!";
    }
}
