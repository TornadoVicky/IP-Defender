using UnityEngine;
using UnityEngine.UI;

public class ImageCycleButton : MonoBehaviour
{
    public Image uiImage; // Reference to the UI Image component
    public Sprite[] images; // Array of sprites representing the images

    private int currentIndex = 0;

    void Start()
    {
        // Initialize the UI image with the first image from the array
        if (uiImage != null && images.Length > 0)
        {
            uiImage.sprite = images[currentIndex];
        }
    }

    public void NextImage()
    {
        // Check if there are images and UI image component assigned
        if (images.Length == 0 || uiImage == null)
        {
            Debug.LogWarning("No images or UI Image component assigned.");
            return;
        }

        // Increment the index to go to the next image
        currentIndex = (currentIndex + 1) % images.Length;

        // Update the UI image with the next image
        uiImage.sprite = images[currentIndex];
    }
}
