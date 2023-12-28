using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageSlideshow : MonoBehaviour
{
    public Image imageComponent;
    public Sprite[] images;
    public float fadeDuration = 1.0f;
    public float displayDuration = 2.0f;

    private int currentIndex = 0;

    private void Start()
    {
        // Start the slideshow
        InvokeRepeating("NextImage", 0, fadeDuration + displayDuration);
    }

    private void NextImage()
    {
        // Crossfade to the next image
        currentIndex = (currentIndex + 1) % images.Length;
        StartCoroutine(Crossfade(images[currentIndex]));
    }

    private IEnumerator Crossfade(Sprite nextImage)
    {
        float elapsedTime = 0;
        Color initialColor = imageComponent.color;
        Color finalColor = new Color(0, 0, 0, 1);

        while (elapsedTime < fadeDuration)
        {
            imageComponent.color = Color.Lerp(initialColor, finalColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        imageComponent.sprite = nextImage;

        elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            imageComponent.color = Color.Lerp(finalColor, initialColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        imageComponent.color = initialColor;
    }
}
