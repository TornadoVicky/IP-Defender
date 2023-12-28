using UnityEngine;
using UnityEngine.UI;

public class ShowImageBasedOnLevel : MonoBehaviour
{
    public int levelToShow; // Set the level for which this image should be visible

    void Start()
    {
        // Find the LevelSelected object in the scene
        GameObject levelSelectedObject = GameObject.Find("LevelSelected");

        if (levelSelectedObject != null)
        {
            // Retrieve the selected level from LevelSelectedScript
            LevelSelectedScript levelSelectedScript = levelSelectedObject.GetComponent<LevelSelectedScript>();
            
            if (levelSelectedScript != null)
            {
                // Check if the selected level matches the level to show
                if (levelSelectedScript.selectedLevel == levelToShow)
                {
                    // Set the image to be visible
                    GetComponent<Image>().enabled = true;
                }
                else
                {
                    // Set the image to be invisible
                    GetComponent<Image>().enabled = false;
                }
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
