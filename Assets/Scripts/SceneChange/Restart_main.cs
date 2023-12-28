using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart_main : MonoBehaviour
{
    public void RestartLevel()
    {
        // Find the LevelSelected object in the scene
        GameObject levelSelectedObject = GameObject.Find("LevelSelected");

        if (levelSelectedObject != null)
        {
            // Retrieve the selected level from LevelSelectedScript
            LevelSelectedScript levelSelectedScript = levelSelectedObject.GetComponent<LevelSelectedScript>();

            if (levelSelectedScript != null)
            {
                // Use the selected level as the scene number to move to
                SceneManager.LoadScene(levelSelectedScript.selectedLevel);
            }
            else
            {
                Debug.LogWarning("LevelSelectedScript not found on LevelSelected object.");
            }
        }
        else
        {
            Debug.LogWarning("LevelSelected object not found in the scene.");
        }
    }
}