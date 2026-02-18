using UnityEngine;
using TMPro; // Needed for TextMeshPro

public class CountdownTimer : MonoBehaviour
{
    [Header("Component References")]
    public TMP_Text timerText; // Drag your Text object here

    [Header("Timer Settings")]
    public float timeRemaining = 600f; // Start time in seconds
    public bool timerIsRunning = false;

    private void Start()
    {
        // Starts the timer automatically when the UI loads
        timerIsRunning = true;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                PerformTimerEndActions();
            }
        }
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        timeToDisplay += 1; // Adds 1 so the timer doesn't show "0" for a whole second
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Formats the text as 00:00
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void PerformTimerEndActions()
    {
        // Optional: Change text color to red or trigger the quit automatically
        timerText.text = "00:00";
        timerText.color = Color.red;
        Debug.Log("Time is up!");
    }
}