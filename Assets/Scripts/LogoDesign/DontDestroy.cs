using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private static int highestSortingOrder = 0;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the scene contains a GameObject with "Tower_M" script
        GameObject towerObject = GameObject.FindWithTag("Tower_M");

        // If a GameObject with "Tower_M" script is found, set the position
        if (towerObject != null)
        {
            transform.position = new Vector3(towerObject.transform.position.x, towerObject.transform.position.y, transform.position.z);
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Find the highest sorting order among all instances
        if (spriteRenderer != null)
        {
            highestSortingOrder = Mathf.Max(highestSortingOrder, spriteRenderer.sortingOrder);
        }

        // Increment the sorting order for the current instance
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = highestSortingOrder + 1;
        }
    }
}
