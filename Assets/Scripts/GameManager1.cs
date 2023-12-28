using UnityEngine;

public class GameManager1 : MonoBehaviour
{
    public GameObject pauseMenu; // Reference to the GameObject you want to activate/deactivate
    public AudioSource exceptionAudioSource; // Reference to the specific AudioSource as an exception

    private bool isPaused = false;

    void Update()
    {
        // Check for user input to toggle pause
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        // Toggle the pause state
        isPaused = !isPaused;

        // Pause or resume the game based on the current state
        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    void PauseGame()
    {
        // Set time scale to 0 to pause the game
        Time.timeScale = 0f;

        // Activate the pause menu GameObject
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }

        // Get all TouchInputHandler instances in the scene when paused
        TouchInputHandler[] touchInputHandlers = FindObjectsOfType<TouchInputHandler>();

        // Iterate through all TouchInputHandler scripts and deactivate them
        foreach (TouchInputHandler touchInputHandler in touchInputHandlers)
        {
            touchInputHandler.enabled = false;
        }

        // Get all AudioSources in the scene
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        // Mute all AudioSources except the exception
        foreach (AudioSource audioSource in allAudioSources)
        {
            if (audioSource != exceptionAudioSource)
            {
                audioSource.mute = true;
            }
            else
            {
                // Set pitch to 0.6 for the exception AudioSource
                audioSource.pitch = 0.6f;
            }
        }

        Debug.Log("Game Paused");
    }

    void ResumeGame()
    {
        // Set time scale back to 1 to resume the game
        Time.timeScale = 1f;

        // Deactivate the pause menu GameObject
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }

        // Get all TouchInputHandler instances in the scene when resumed
        TouchInputHandler[] touchInputHandlers = FindObjectsOfType<TouchInputHandler>();

        // Iterate through all TouchInputHandler scripts and activate them
        foreach (TouchInputHandler touchInputHandler in touchInputHandlers)
        {
            touchInputHandler.enabled = true;
        }

        // Get all AudioSources in the scene
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        // Unmute all AudioSources except the exception
        foreach (AudioSource audioSource in allAudioSources)
        {
            if (audioSource != exceptionAudioSource)
            {
                audioSource.mute = false;
            }
            else
            {
                // Reset pitch to 1 for the exception AudioSource
                audioSource.pitch = 1f;
            }
        }

        Debug.Log("Game Resumed");
    }
}
