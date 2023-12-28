using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelect : MonoBehaviour
{
    public Button[] levelButtons;
    public TMP_Text tutorialText;
    public GameObject slideshowObject;
    public float delayBeforeLoading = 2.0f;
    public float delayBeforeCheckingInput = 1.0f; // Introduce a delay before checking for skip input
    public int levelnumber = 0;
    public int nextToLoad = 0;

    private bool skipSlideshow = false; // Flag to check if the slideshow should be skipped

    public void StartGame(int buttonIndex)
    {
        StartCoroutine(StartGameCoroutine(buttonIndex));
    }

    private IEnumerator StartGameCoroutine(int buttonIndex)
    {
        // Disable the clicked button
        levelButtons[buttonIndex].interactable = false;

        // Deactivate the child of the clicked button
        Transform buttonTransform = levelButtons[buttonIndex].transform;
        foreach (Transform child in buttonTransform)
        {
            child.gameObject.SetActive(false);
        }

        // Deactivate all other buttons
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i != buttonIndex)
            {
                levelButtons[i].gameObject.SetActive(false);
            }
        }

        // Deactivate the TMP text
        tutorialText.gameObject.SetActive(false);

        // Activate the ImageSlideshow script
        ImageSlideshow slideshowScript = slideshowObject.GetComponent<ImageSlideshow>();
        if (slideshowScript != null)
        {
            slideshowScript.enabled = true;
        }
        else
        {
            Debug.LogWarning("ImageSlideshow script not found on the specified GameObject.");
        }

        LevelSelectedScript levelSelectedScript = FindObjectOfType<LevelSelectedScript>();
        if (levelSelectedScript != null)
        {
            levelSelectedScript.selectedLevel = levelnumber;
        }
        else
        {
            Debug.LogWarning("LevelSelectedScript not found.");
        }

        // Wait for a delay before checking for skip input
        yield return new WaitForSeconds(delayBeforeCheckingInput);

        float startTime = Time.time; // Record the start time

        // Check for input to skip the slideshow within the time limit
        while (!skipSlideshow && Time.time - startTime < delayBeforeLoading)
        {
            if (Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                skipSlideshow = true;
            }

            yield return null;
        }

        // Load the next scene
        SceneManager.LoadScene(nextToLoad);
    }
}
