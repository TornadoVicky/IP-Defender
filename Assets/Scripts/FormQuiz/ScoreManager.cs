using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public GameObject[] questionManagers; // Reference to the Manager GameObjects containing ButtonController scripts
    public TextMeshProUGUI questionScoreTextMeshPro; // Assign this in the Unity Editor
    public TextMeshProUGUI totalScoreTextMeshPro; // Assign this in the Unity Editor

    private int[] questionScores;

    private void Start()
    {
        // Initialize the array to store question scores
        questionScores = new int[questionManagers.Length];
    }

    public void OnScoreButtonClick()
    {
        int totalScore = 0;

        for (int i = 0; i < questionManagers.Length; i++)
        {
            ButtonController buttonController = questionManagers[i].GetComponent<ButtonController>();
            if (buttonController != null)
            {
                int selectedPoints = buttonController.GetSelectedPoints();
                // Store the selected points for each question
                questionScores[i] = selectedPoints;

                // Accumulate total score
                totalScore += selectedPoints;

                // You can store or process the selectedPoints for each question as needed
                Debug.Log($"{questionManagers[i].name} - Score: {selectedPoints} points");
            }
            else
            {
                Debug.LogWarning($"ButtonController not found on {questionManagers[i].name}.");
            }
        }

        // Display question-wise scores in the TextMeshPro
        UpdateQuestionScoreText();

        // Display the total score in the TextMeshPro
        totalScoreTextMeshPro.text = $"Total Score: {totalScore} points";
    }

    private void UpdateQuestionScoreText()
    {
        // Display question-wise scores in the TextMeshPro
        string scoreText = "Question Scores:\n";
        for (int i = 0; i < questionScores.Length; i++)
        {
            scoreText += $"{questionManagers[i].name}: {questionScores[i]} points\n";
        }
        questionScoreTextMeshPro.text = scoreText;
    }
}
