using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauser : MonoBehaviour
{
    public void UnPause()
    {
        FindObjectOfType<SettingsController>().PauseGame(false);
    }
}
