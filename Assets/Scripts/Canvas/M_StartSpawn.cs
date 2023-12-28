using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class M_StartSpawn : MonoBehaviour
{
    public M_SuccessManager successManager;
    public GameObject objectToActivate;
    private Button button; // Reference to the button component

    void Start()
    {
        button = GetComponent<Button>(); // Get the button component
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick); // Add the OnButtonClick method as a listener
        }
        else
        {
            Debug.LogWarning("Button component not found!");
        }
    }

    public void OnButtonClick()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);

            if (button != null)
            {
                button.interactable = false; // Disable the button
                StartCoroutine(DisableButtonDelayed());
            }
            if (successManager != null)
            {
                successManager.StartGame(); // Start the game
            }
        }
        else
        {
            Debug.LogWarning("No GameObject assigned to activate!");
        }
    }

    IEnumerator DisableButtonDelayed()
    {
        yield return new WaitForSeconds(1.0f); // Adjust the delay as needed
        button.gameObject.SetActive(false); // Hide the button
    }
}
