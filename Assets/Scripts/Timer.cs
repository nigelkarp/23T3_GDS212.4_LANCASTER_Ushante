using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    //credit chat gpt for the start of this script

    private float timeRemaining;
    private bool isRunning = false;

    private Action onTimerComplete;

    // Start the timer with the specified duration and callback function
    public void StartTimer(float duration, Action onComplete)
    {
        timeRemaining = duration;
        onTimerComplete = onComplete;
        isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0f)
            {
                // Timer has reached zero
                isRunning = false;
                onTimerComplete?.Invoke(); // Invoke the callback function if it's not null
            }
        }
    }

    // Stop the timer
    public void StopTimer()
    {
        isRunning = false;
    }
}
