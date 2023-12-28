using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component

    void Update()
    {
        // Check if the reference to the TextMeshProUGUI component is assigned
        if (scoreText != null)
        {
            // Update the text of the TextMeshProUGUI component with the current value of totalScore
            scoreText.text = "Total Score: " + Scoring.totalScore.ToString();
        }
    }
}