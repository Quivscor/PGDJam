using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauser : MonoBehaviour
{
    public Teleport teleportToBase;

    public void BackToSanctuarium()
    {
        SettingsController.Instance.PauseGame(false);
        ScreenFadeController.Instance.FadeOutAndInWithTeleport(teleportToBase.teleportDestination);

    }
    public void UnPause()
    {
        SettingsController.Instance.PauseGame(false);
    }

    public void ShowAbout()
    {
        SettingsController.Instance.ToggleAboutCanvas(true);
    }

    public void HideAbout()
    {
        SettingsController.Instance.ToggleAboutCanvas(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
