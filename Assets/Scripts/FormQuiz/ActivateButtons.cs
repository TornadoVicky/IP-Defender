using UnityEngine;
using UnityEngine.UI;

public class ActivateButtons : MonoBehaviour
{
    public Button buttonToClick; // Assign this button in the Unity Editor
    public Button button1; // Assign this button in the Unity Editor
    public Button button2; // Assign this button in the Unity Editor

    private void Start()
    {
        // Add a click listener to the button you want to click
        buttonToClick.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        // Activate the other two buttons
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);

        // You can add additional logic or actions here if needed
        Debug.Log("Button clicked! Activating buttons 1 and 2.");
    }
}
