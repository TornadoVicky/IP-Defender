using UnityEngine;

public class FlipForward : MonoBehaviour
{
    public Animator targetAnimator; // Reference to the Animator component you want to control
    public string triggerName = "YourTriggerName"; // Replace with the actual trigger name
    private PageManager pageManager; // Reference to the PageManager script

    void Start()
    {
        // Ensure valid references are set in the Inspector
        if (targetAnimator == null)
        {
            Debug.LogError("Target Animator is not assigned!");
        }

        pageManager = FindObjectOfType<PageManager>(); // Assuming there's only one PageManager in the scene
    }

    public void OnButtonClick()
    {
        // Check if the Animator component and PageManager are assigned
        if (targetAnimator != null && pageManager != null)
        {
            // Toggle the specified trigger in the Animator
            targetAnimator.SetBool(triggerName, !targetAnimator.GetBool(triggerName));

            // Flip forward using the PageManager
            pageManager.FlipForward();
        }
        else
        {
            Debug.LogError("Target Animator or PageManager is not assigned!");
        }
    }
}
