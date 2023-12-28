using UnityEngine;

public class LevelSelectedScript : MonoBehaviour
{
    public int selectedLevel = 0; // Default to 0 or another value indicating not selected

    void Awake()
    {
        // Check if another instance of LevelSelectedScript already exists
        LevelSelectedScript[] existingInstances = FindObjectsOfType<LevelSelectedScript>();
        
        foreach (LevelSelectedScript instance in existingInstances)
        {
            // Destroy all other instances (except for this one)
            if (instance != this)
            {
                Destroy(instance.gameObject);
            }
        }

        // Make sure this GameObject persists between scenes
        DontDestroyOnLoad(gameObject);
    }
}
