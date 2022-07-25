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

    // Player reset
    [SerializeField] Transform playerOrigin;
    [SerializeField] Transform playerTransform;
    [SerializeField] Vector3 defaultPosition;

    [SerializeField] PlayerData playerData;

    void Start()
    {
        endMenu.SetActive(false);
        SetTimeScale(1);
        

        if(playerData)
            playerOrigin.position = playerData.originPosition;
    }

    public void HandleLoose(string reason)
    {
        SetTimeScale(0.5f);
        StartCoroutine(ShowMenuAfterTime(reason, 2));
    }
    IEnumerator ShowMenuAfterTime(string title, float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        if (creator)
            creator.DestroyAllCreatedObjects();
        endMenu.SetActive(true);
        menuTitle.text = title;

        scriptsToActivate.ForEach(x => x.enabled = true);
        objectsToHide.ForEach(x => x.SetActive(false));
        var dots = FindObjectsOfType<BhapticsDotPoint>();
        foreach (var dot in dots)
            dot.enabled = false;

        SetTimeScale(0);
    }
    public void HandleWin()
    {
        HandleLoose("You won!");
    }
    public void PlayerReset()
    {
        playerOrigin.position += defaultPosition - playerTransform.position;

        if(playerData)
            playerData.originPosition = playerOrigin.position;
    }

    static void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
        List<AudioSource> audios = new(FindObjectsOfType<AudioSource>());
        audios.ForEach(x => x.pitch = scale);
    }
}
