using UnityEngine;
using System.Collections;

public class DisableAnimatorAfterDelay : MonoBehaviour
{
    public float startDelay = 2.0f; // Delay before activating the Animator
    public float deactivationDelay = 5.0f; // Delay before deactivating the Animator

    private Animator animator;

    void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();

        // Check if the Animator component is found
        if (animator == null)
        {
            Debug.LogError("Animator component not found on GameObject: " + gameObject.name);
        }
        else
        {
            // Start a coroutine to control the activation and deactivation of the Animator
            StartCoroutine(ActivateDeactivateAnimatorCoroutine());
        }
    }

    IEnumerator ActivateDeactivateAnimatorCoroutine()
    {
        // Wait for the start delay before activating the Animator
        yield return new WaitForSeconds(startDelay);

        // Activate the Animator
        animator.enabled = true;

        // Optionally, you can perform additional actions here after activating the Animator

        // Wait for the deactivation delay
        yield return new WaitForSeconds(deactivationDelay);

        // Deactivate the Animator
        animator.enabled = false;

        // Optionally, you can perform additional actions here after deactivating the Animator
    }
}
