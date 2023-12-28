using UnityEngine;
using UnityEngine.UI;

public class ToggleGameObject : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject additionalObject; // New GameObject to be deactivated
    public Button toggleButton;

    void Start()
    {
        // Assign the method to be called when the button is clicked
        toggleButton.onClick.AddListener(ToggleObjectActivation);
    }

    void ToggleObjectActivation()
    {
        // Toggle the activation state of the target object
        if (targetObject != null)
        {
            targetObject.SetActive(!targetObject.activeSelf);
        }

        // Deactivate the additional object
        if (additionalObject != null)
        {
            additionalObject.SetActive(false);
        }
    }
}
