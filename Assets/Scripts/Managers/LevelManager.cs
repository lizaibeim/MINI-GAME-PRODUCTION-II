using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public delegate void ManagerEvent();
    public ManagerEvent restartEvent;
    public ManagerEvent mainMenuEvent;

    GameManager gameManager = new GameManager();

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager.SaveLevel();

    }

    public void Death()
    {
        StartCoroutine("DeathTimer");
    }
    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(2);

        restartEvent?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    IEnumerator NextLevelTimer()
    {
        yield return new WaitForSeconds(3);

        restartEvent?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void GoToMainMenu() {
        mainMenuEvent?.Invoke(); 
        PauseManager.isPaused = false;
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(1);
    }

    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings)
            StartCoroutine("NextLevelTimer");
    }

    public void ResetGame()
    {
        restartEvent?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
