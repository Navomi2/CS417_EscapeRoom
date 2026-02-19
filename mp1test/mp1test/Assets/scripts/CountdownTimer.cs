using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [Header("Component References")]
    public TMP_Text timerText;
    public Transform player;      // Drag your XR Rig / Player object here
    public Transform losePosition; // Drag your "Lose" empty GameObject here

    public Transform playerUI;

    [Header("Timer Settings")]
    public float timeRemaining = 60f;
    public bool timerIsRunning = false;

    private void Start()
    {
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
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void PerformTimerEndActions()
    {
        // UI Updates
        timerText.text = "00:00";
        timerText.color = Color.red;
        Debug.Log("Time is up! Teleporting...");

        // TELEPORT LOGIC
        if (player != null && losePosition != null)
        {
            // Temporarily disable CharacterController (if present)
            // This prevents the physics system from snapping the player back to the old spot
            CharacterController cc = player.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;
            }

            // Move the Player
            player.position = losePosition.position;
            player.rotation = losePosition.rotation;

            // move the ui
            Vector3 uiPos = losePosition.position;
            uiPos.x -= 3f;
            uiPos.z -= 3f;

            playerUI.position = uiPos;
            playerUI.LookAt(losePosition);

            playerUI.Rotate(0f, 180f, 0f);

            // Re-enable CharacterController
            if (cc != null)
            {
                cc.enabled = true;
            }
        }
        else
        {
            Debug.LogError("Player or LosePosition is not assigned in the Inspector!");
        }
    }
}