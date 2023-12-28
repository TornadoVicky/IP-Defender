using UnityEngine;
using TMPro;

public class ComboDisplay : MonoBehaviour
{
    public TextMeshProUGUI comboText; // Reference to the UI text element for displaying the combo
    public float comboDisplayDuration = 1f; // Duration to display the combo
    private int comboCount = 0;
    private float comboTimer = 0f;

    void Update()
    {
        // Update the combo timer
        comboTimer += Time.deltaTime;

        // If combo timer exceeds the display duration, reset the combo count
        if (comboTimer >= comboDisplayDuration)
        {
            comboCount = 0;
            comboTimer = 0f;
            UpdateComboText();
        }
    }

    public void IncreaseCombo()
    {
        // Increase the combo count
        comboCount++;

        // Update the combo timer
        comboTimer = 0f;

        // Update the UI text
        UpdateComboText();
    }

    void UpdateComboText()
    {
        // Update the UI text to display the current combo count
        comboText.text = "Combo: " + comboCount;
    }
}
