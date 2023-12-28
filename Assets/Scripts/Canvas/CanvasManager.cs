using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance; // Singleton pattern

    public Canvas[] canvases; // Array to store all the canvases

    private Canvas activeCanvas; // Reference to the currently active canvas

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleCanvas(int canvasIndex)
    {
        Canvas canvasToToggle = canvases[canvasIndex];

        if (activeCanvas != null && activeCanvas != canvasToToggle)
        {
            activeCanvas.gameObject.SetActive(false);
        }

        activeCanvas = canvasToToggle;
        canvasToToggle.gameObject.SetActive(!canvasToToggle.gameObject.activeSelf);
    }
}