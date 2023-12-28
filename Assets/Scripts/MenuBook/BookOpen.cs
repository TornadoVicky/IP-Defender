using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BookOpen : MonoBehaviour
{
    public Animator targetAnimator; // Reference to the Animator component you want to control
    public List<GameObject> targetObjects = new List<GameObject>(); // List of target GameObjects to toggle
    public string triggerName = "YourTriggerName"; // Replace with the actual trigger name
    public float activationDelay = 1.0f; // Delay before activation
    public bool activateObjects = true; // Set this in the Inspector based on whether you want to activate or deactivate

    private PageManager pageManager; // Reference to the PageManager script

    void Start()
    {
        // Ensure valid references are set in the Inspector
        if (targetAnimator == null)
        {
            Debug.LogError("Target Animator is not assigned!");
        }
        if (targetObjects.Count == 0)
        {
            Debug.LogError("No target objects assigned!");
        }

        // Find the PageManager script in the scene
        pageManager = FindObjectOfType<PageManager>();
        if (pageManager == null)
        {
            Debug.LogError("PageManager script not found in the scene!");
        }
    }

    public void OnButtonClick()
    {
        // Check if the Animator component and PageManager are assigned
        if (targetAnimator != null && pageManager != null)
        {
            StartCoroutine(ActivateObjectsCoroutine());
        }
        else
        {
            Debug.LogError("Target Animator or PageManager is not assigned!");
        }
    }

    IEnumerator ActivateObjectsCoroutine()
    {
        // Toggle the specified trigger in the Animator
        targetAnimator.SetBool(triggerName, !targetAnimator.GetBool(triggerName));

        if (activateObjects)
        {
            // Wait for activation delay
            yield return new WaitForSeconds(activationDelay);
        }

        // Activate or deactivate immediately based on the boolean
        foreach (var targetObject in targetObjects)
        {
            if (targetObject != null)
            {
                targetObject.SetActive(activateObjects);
            }
            else
            {
                Debug.LogError("One of the target objects is not assigned!");
            }
        }

        // Log current book state
        Debug.Log("Book is " + (activateObjects ? "open" : "closed"));

        // Notify the PageManager about the book state
        pageManager.SetBookOpenState(activateObjects);

        // Log if PageManager received the toggle
        Debug.Log("PageManager received toggle from BookOpen script");
        
        activateObjects = !activateObjects;
    }
}
