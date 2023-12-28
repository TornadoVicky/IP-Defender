using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic; // Import the System.Collections.Generic namespace for using List<T>

public class QuizActive : MonoBehaviour
{
    public List<GameObject> objectsToActivate; // Reference to the list of GameObjects you want to activate

    private void Start()
    {
        // Add a click listener to the button
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (objectsToActivate != null && objectsToActivate.Count > 0)
        {
            // Activate each referenced GameObject in the list
            foreach (GameObject obj in objectsToActivate)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                    Debug.Log($"{obj.name} activated!");
                }
                else
                {
                    Debug.LogWarning("Null GameObject reference found in the list.");
                }
            }

            // Deactivate the button's GameObject
            gameObject.SetActive(false);

            Debug.Log("Button GameObject deactivated.");
        }
        else
        {
            Debug.LogWarning("No GameObject references assigned for activation.");
        }
    }
}
