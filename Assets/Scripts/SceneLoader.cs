using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Specify the scene name or index you want to load
    public int sceneToLoad;

    // Specify the time scale value to set
    public float newTimeScale = 1.0f;

    // Call this method to load the specified scene and set the time scale
    public void LoadSceneAndSetTimeScale()
    {
        // Set the new time scale
        Time.timeScale = newTimeScale;

        // Load the specified scene
        SceneManager.LoadScene(sceneToLoad);
    }
}