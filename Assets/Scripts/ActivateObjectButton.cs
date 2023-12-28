using UnityEngine;
using UnityEngine.UI;

public class ActivateObjectButton : MonoBehaviour
{
    public Button activateButton; // Reference to the activate button
    public GameObject targetObject; // Reference to the object to activate

    void Start()
    {
        // Ensure that the activate button and target object are set in the Inspector
        if (activateButton == null)
        {
            Debug.LogError("Activate Button is not set in the inspector!");
        }

        if (targetObject == null)
        {
            Debug.LogError("Target Object is not set in the inspector!");
        }

        // Attach the Click event listener to the activate button
        activateButton.onClick.AddListener(ActivateTargetObject);
    }

    void ActivateTargetObject()
    {
        // Activate the target object
        if (targetObject != null)
        {
            targetObject.SetActive(true);
        }
    }
}
