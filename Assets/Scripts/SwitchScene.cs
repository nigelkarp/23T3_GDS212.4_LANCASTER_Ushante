using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void playGame(string Game)
    {
        SceneManager.LoadScene(Game);
    }

    //later dont use this to end the game on a loss, make it so that the game throws up a lose screen
    public void stopGame(string StartMenu) 
    {
        SceneManager.LoadScene(StartMenu);
    }
}
