using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Timer _timer;             //reference to the timer game object
    [SerializeField] private TMP_Text _timerText;      //reference to the timer UI
    [SerializeField] private GameObject _itemCollectionManager;

    [SerializeField] private GameObject winWindow;      //reference to win window
    [SerializeField] private GameObject loseWindow;      //reference to lose window

    // Reference to fish game objects

    private void Start()
    {
        _timer.StartTimer(50f, GameEnd);
        winWindow.SetActive(false);
        loseWindow.SetActive(false);
    }

    private void Update()
    {
        UpdateTimerUI();
        GameEnd();
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
        int currentScore = _itemCollectionManager.GetComponent<ItemCollection>().ReturnCurrentScore();

        if (!_timer.isRunning && currentScore < 10)
        {
            loseWindow?.SetActive(true);
        }
        else if (currentScore >= 10)
        {
            winWindow.SetActive(true);
        }
    }
}
