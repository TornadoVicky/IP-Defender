using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tower_M : MonoBehaviour
{
    public Slider healthSlider;
    public float detectionRadius = 10f;
    public int maxIntegrity = 100;
    private int currentIntegrity;
    public Sprite newSprite;
    public int defeatScene;
    public float checkInterval = 0.5f; // Set the interval in seconds
    private float timer = 0f;
    private bool hasFailed = false;

    public GameObject transitionObject; // Reference to the transitionObject

    void Start()
    {
        currentIntegrity = maxIntegrity;
        UpdateHealthSlider();
    }

    void Update()
    {
        if (hasFailed)
        {
            ShowFailScreen();
        }

        timer += Time.deltaTime;

        if (timer >= checkInterval)
        {
            CheckForEnemiesInRange();
            timer = 0f; // Reset the timer
        }
    }

    void CheckForEnemiesInRange()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance <= detectionRadius)
            {
                TakeDamage(5); // Assuming each enemy does 1 damage per frame
                break; // Stop checking once one enemy is found in range
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentIntegrity -= damage;

        if (currentIntegrity <= 0)
        {
            currentIntegrity = 0;
            DestroyTower();
        }

        UpdateHealthSlider();
    }

    void UpdateHealthSlider()
    {
        healthSlider.value = (float)currentIntegrity / maxIntegrity;
    }

    void DestroyTower()
    {
        GameObject levelObject = GameObject.FindGameObjectWithTag("Level");

        if (levelObject != null)
        {
            SpriteRenderer spriteRenderer = levelObject.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                // Load the new sprite for the Level GameObject
                spriteRenderer.sprite = newSprite; // Replace YourNewSprite with your actual sprite reference
            }

            ActivateTransitionAndLoadFailScene();
        }
    }

    void ActivateTransitionAndLoadFailScene()
    {
        if (transitionObject != null)
        {
            transitionObject.SetActive(true);
        }

        StartCoroutine(LoadFailSceneDelayed());
    }

    IEnumerator LoadFailSceneDelayed()
    {
        yield return new WaitForSeconds(2.0f); // Adjust the delay as needed
        SceneManager.LoadScene(defeatScene);
    }

    void ShowFailScreen()
    {
        // Perform any additional actions needed when showing the fail screen
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
