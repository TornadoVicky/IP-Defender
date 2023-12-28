using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class WallHealth : MonoBehaviour
{
    public int maxIntegrity = 20;
    public int currentIntegrity;
    public float detectionRadius = 5f;
    public float vignetteIntensityIncrease = 0.2f; // Adjust this value as needed
    public Sprite replacementSprite; // Assign the replacement sprite in the Inspector

    private PostProcessVolume postProcessVolume;
    private Vignette vignetteLayer;
    private SpriteRenderer spriteRenderer;
    private bool hasIncreasedVignette = false;

    private CameraShake cameraShake;
    private Animator wallAnimator; // Reference to the Animator component

    void Start()
    {
        currentIntegrity = maxIntegrity;

        // Find the Post Process Volume in the scene
        postProcessVolume = FindObjectOfType<PostProcessVolume>();

        // If Post Process Volume is found, get the Vignette layer
        if (postProcessVolume != null)
        {
            postProcessVolume.profile.TryGetSettings(out vignetteLayer);
        }
        else
        {
            Debug.LogWarning("Post Process Volume not found in the scene. Vignette effect will not be applied.");
        }

        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Find the CameraShake script in the scene
        cameraShake = FindObjectOfType<CameraShake>();

        // Get the Animator component
        wallAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        CheckForEnemiesInRange();
    }

    void CheckForEnemiesInRange()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance <= detectionRadius)
            {
                TakeDamage(1);
                break;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentIntegrity -= damage;

        if (currentIntegrity <= 0 && !hasIncreasedVignette)
        {
            currentIntegrity = 0;
            DestroyWall();
        }
    }

    void DestroyWall()
    {
        // Increase vignette intensity if the Post Process Volume and Vignette layer are available
        if (vignetteLayer != null)
        {
            vignetteLayer.intensity.value += vignetteIntensityIncrease;
            hasIncreasedVignette = true; // Set the flag to true after increasing intensity
        }

        // Shake the camera using CameraShake script
        if (cameraShake != null)
        {
            cameraShake.ShakeIt();
        }

        // Change the sprite to the replacement sprite
        if (replacementSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = replacementSprite;
        }

        // Activate the animator if available
        if (wallAnimator != null)
        {
            wallAnimator.enabled = true;
            //wallAnimator.SetTrigger("Activate"); // Assuming there's a trigger named "Activate"
        }

        // Additional effects or logic can be added here if needed
    }
}
