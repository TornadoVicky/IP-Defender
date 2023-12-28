using UnityEngine;
using System.Collections;

public class DeactivateAfterDuration : MonoBehaviour
{
    public float deactivationDelay = 5.0f; // Customize the duration before deactivating the GameObject

    void Start()
    {
        // Start a coroutine to deactivate the GameObject after the specified delay
        StartCoroutine(DeactivateObjectCoroutine());
    }

    IEnumerator DeactivateObjectCoroutine()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(deactivationDelay);

        // Deactivate the GameObject
        gameObject.SetActive(false);
    }
}
