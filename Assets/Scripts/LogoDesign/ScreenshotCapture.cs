using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScreenshotCapture : MonoBehaviour
{
    public float captureDelay = 1f;
    public GameObject[] objectsToDisable;
    public Button buttonToDisable;
    public TextMeshProUGUI textToDisable;
    public GameObject targetObject; // Reference to the existing GameObject in the scene
    public int nextScene = 0;

    private SpriteRenderer spriteRenderer;

    public void CaptureScreenshot()
    {
        StartCoroutine(TakeScreenshotAfterDelay());
    }

    private IEnumerator TakeScreenshotAfterDelay()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }

        buttonToDisable.interactable = false;
        Color buttonColor = buttonToDisable.image.color;
        buttonColor.a = 0f;
        buttonToDisable.image.color = buttonColor;

        textToDisable.enabled = false;
        Color textColor = textToDisable.color;
        textColor.a = 0f;
        textToDisable.color = textColor;

        yield return new WaitForSeconds(captureDelay);

        yield return new WaitForEndOfFrame();

        Texture2D screenshotTexture = ScreenCapture.CaptureScreenshotAsTexture();

        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(true);
        }

        buttonToDisable.interactable = true;
        buttonColor.a = 1f;
        buttonToDisable.image.color = buttonColor;

        textToDisable.enabled = true;
        textColor.a = 1f;
        textToDisable.color = textColor;

        // Apply the screenshot texture to the existing GameObject with SpriteRenderer
        ApplyTextureToSpriteRenderer(targetObject, screenshotTexture);

        // Move to the next scene after capturing and displaying the screenshot
        MoveToNextScene();
    }

    private void ApplyTextureToSpriteRenderer(GameObject obj, Texture2D texture)
    {
        // Assuming the GameObject has a SpriteRenderer component
        spriteRenderer = obj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            spriteRenderer.sprite = sprite;
        }
        // Add more checks or adjust this part based on your actual GameObject structure
    }

    private void MoveToNextScene()
    {
        // Find the LevelSelectedScript in the scene
        LevelSelectedScript levelSelectedScript = FindObjectOfType<LevelSelectedScript>();

        // Use the selectedLevel from LevelSelectedScript to determine the next scene to load
        //int nextScene = levelSelectedScript != null ? levelSelectedScript.selectedLevel : 2;

        // Load the next scene
        SceneManager.LoadScene(nextScene);
    }
}
