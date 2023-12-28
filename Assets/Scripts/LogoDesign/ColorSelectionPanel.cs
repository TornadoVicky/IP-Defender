using UnityEngine;
using UnityEngine.UI;

public class ColorSelectionPanel : MonoBehaviour
{
    public Image colorPreview;
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    private void Start()
    {
        // Initialize sliders and preview color
        redSlider.onValueChanged.AddListener(UpdateColor);
        greenSlider.onValueChanged.AddListener(UpdateColor);
        blueSlider.onValueChanged.AddListener(UpdateColor);

        // Set initial color
        UpdateColor(0.5f);
    }

    private void UpdateColor(float value)
    {
        // Update color preview
        Color selectedColor = new Color(redSlider.value, greenSlider.value, blueSlider.value);
        colorPreview.color = selectedColor;
    }
}
