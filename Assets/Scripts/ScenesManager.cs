using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;
    
    public void Awake()
    {
        Instance = this;
    }

    public class Scenes
    {
        public const string MainMenu = "MainMenuScene";
        public const string Game = "GameScene";
        public const string Help = "HelpScene";
        public const string EndGame = "EndGameScene";
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(Scenes.Game);
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(Scenes.MainMenu);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
