using UnityEngine;

public class MenuOpen : MonoBehaviour
{
    public Animator targetAnimator; // Reference to the Animator component you want to control
    public string triggerName = "YourTriggerName"; // Replace with the actual trigger name

    private bool animatorEnabled = false; // Track whether the Animator is enabled

    void Start()
    {
        // Ensure valid references are set in the Inspector
        if (targetAnimator == null)
        {
            Debug.LogError("Target Animator is not assigned!");
        }
    }

    public void OnButtonClick()
    {
        // Check if the Animator component is assigned
        if (targetAnimator != null)
        {
            if (!animatorEnabled)
            {
                // Enable the Animator if it hasn't been enabled yet
                targetAnimator.enabled = true;
                animatorEnabled = true;
            }
            else
            {
                // Toggle the specified trigger in the Animator
                targetAnimator.SetBool(triggerName, !targetAnimator.GetBool(triggerName));
            }
        }
        else
        {
            Debug.LogError("Target Animator is not assigned!");
        }
    }
}
