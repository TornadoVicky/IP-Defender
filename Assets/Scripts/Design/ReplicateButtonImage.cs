using UnityEngine;
using UnityEngine.UI;

public class ReplicateButtonImage : MonoBehaviour
{
    public Button sourceButton; // Reference to the button with the source image
    public Image targetImage;   // Reference to the image to replicate the button's image
    public TargetObject targetObject; // Reference to the target object
    public int variable;

    void Start()
    {
        // Ensure that the button, target image, and target object are set in the Inspector
        if (sourceButton == null)
        {
            Debug.LogError("Source Button is not set in the inspector!");
        }

        if (targetImage == null)
        {
            Debug.LogError("Target Image is not set in the inspector!");
        }

        if (targetObject == null)
        {
            Debug.LogError("Target Object is not set in the inspector!");
        }

        // Attach the Click event listener to the button
        sourceButton.onClick.AddListener(ReplicateButtonImageToTarget);
    }

    void ReplicateButtonImageToTarget()
    {
        // Replicate the sprite of the button's image to the target image's sprite
        if (sourceButton != null && targetImage != null)
        {
            targetImage.sprite = sourceButton.image.sprite;
        }

        if (targetObject != null)
        {
            // Manipulate the custom variable of the target object
            targetObject.ManipulateTargetObject(variable);
        }
        else
        {
            Debug.LogError("Target Object reference is not set!");
        }
    }
}
