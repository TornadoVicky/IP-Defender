using UnityEngine;
using UnityEngine.UI;

public class ToggleObjects : MonoBehaviour
{
    public GameObject[] objectsToToggle;
    private bool isToggled = false;

    public void ToggleObjectsState()
    {
        isToggled = !isToggled; // Toggle the state on each click

        // Toggle the active state of each specified GameObject
        foreach (GameObject obj in objectsToToggle)
        {
            obj.SetActive(!isToggled);
        }
    }
}
