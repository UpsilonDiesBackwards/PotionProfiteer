using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject MainMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseCheck();
        }
    }

    public void PauseCheck()
    {
        if (gameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
    void Resume()
    {
        Time.timeScale = 1;
        MainMenu.SetActive(false);
        gameIsPaused = false;
    }

    void Pause()
    {
        Time.timeScale = 0;
        MainMenu.SetActive(true);
        gameIsPaused = true;
    }


    public Animator transition;
    float transitionTime = 1f;

    public void ResetFunction()
    {
        Resume();
        StartCoroutine(LoadLevel(0));
    }

    public void RestartFunction()
    {
        Resume();
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("StartCrossfade");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}