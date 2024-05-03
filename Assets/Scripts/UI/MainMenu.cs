using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //this is where the code to create the save for the player should go
        SceneManager.LoadSceneAsync("MainScene");
        //the code above is what will load the game screen
    }
 

    public void QuitGame()
    {
        Application.Quit();//allows the player to quit the game

    }

}
