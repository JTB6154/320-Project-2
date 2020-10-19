using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneScript : MonoBehaviour
{ 
    //Load main game
    public void LoadGameScene(string SceneName)
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }


    //Quit the Game
    public void QuitButton()
    {
        Application.Quit();
    }

    //Load Options
}
