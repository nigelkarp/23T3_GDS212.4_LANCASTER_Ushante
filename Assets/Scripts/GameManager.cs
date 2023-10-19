using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Timer _timer;             //reference to the timer game object
    [SerializeField] private TMP_Text _timerText;      //reference to the timer UI

    private void Start()
    {
        _timer.StartTimer(30f, GameEnd);
    }

    private void Update()
    {
        UpdateTimerUI();
    }

    // Function to append time to timer text
    public void UpdateTimerUI()
    {
        // Check if the timer is running
        if (_timer.isRunning)
        {
            // Get the time remaining from the Timer script
            float timeRemaining = _timer.timeRemaining;

            // Format the time as minutes and seconds
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);

            // Update the TMP Text component with the formatted time
            _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void GameEnd()
    {
        Debug.Log("Game ended");
    }
}
