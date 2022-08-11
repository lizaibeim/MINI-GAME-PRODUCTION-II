using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool isPaused;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void OpenMenu()
    {
        if (isPaused)
        {
            CloseMenu();
            return;
        }

        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
        Time.timeScale = 0;
    }

    public void CloseMenu()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
        Time.timeScale = 1;
    }
}
