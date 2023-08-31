using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool IsGamePaused;
    public Sprite playSprite;
    public Sprite pauseSprite;
    public Image playButton;
    public GameObject pauseMenuUI;

    public void Toggle()
    {
        if (IsGamePaused)
        {
            Resume();
        }
        else 
        {
            Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        playButton.sprite = playSprite;
        IsGamePaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        playButton.sprite = pauseSprite;
        IsGamePaused = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
    }

    public void OpenMainMenu()
    {
        Time.timeScale = 1f;
        ScenesManager.Instance.OpenMainMenu();
    }
}
