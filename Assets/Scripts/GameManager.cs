using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Timer timer;             //reference to the timer game object
    public TMP_Text timerText;      //reference to the timer UI

    private void Start()
    {
        timer.StartTimer(30f, GameEnd);
    }

    public void updateTimerUI()
    {

    }

    public void GameEnd()
    {
        Debug.Log("Game ended");
    }
}
