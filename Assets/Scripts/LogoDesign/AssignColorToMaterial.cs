using UnityEngine;
using UnityEngine.UI;

public class AssignColorToMaterial : MonoBehaviour
{
    public Material targetMaterial; // Reference to the material to be assigned
    public Image imageComponent; // Reference to the Image component

    void Start()
    {
        // Get the Image component attached to the same GameObject
        imageComponent = GetComponent<Image>();

        if (imageComponent == null)
        {
            Debug.LogError("Image component not found on the GameObject.");
        }

        if (targetMaterial == null)
        {
            Debug.LogError("Target material is not assigned.");
        }
        else
        {
            // Assign the color to the target material
            AssignColor();
        }
    }

    void AssignColor()
    {
        // Get the color of the Image component's material
        Color imageColor = imageComponent.material.color;

        // Assign the color to the target material
        targetMaterial.color = imageColor;

        Debug.Log("Color assigned to the target material: " + imageColor);
    }
}
