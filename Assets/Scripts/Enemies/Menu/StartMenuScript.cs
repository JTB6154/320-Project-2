using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    //Load main game
    public void LoadGameScene(string SceneName)
    {
        Time.timeScale = 1f;
        GameStats.Instance.runTutorial = true;
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }


    //Quit the Game
    public void QuitButton()
    {
        Application.Quit();
    }

    public void LoadGameWithoutTutorial( string SceneName)
    {
        Time.timeScale = 1f;
        GameStats.Instance.runTutorial = false;
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }

    //Load Options
}
