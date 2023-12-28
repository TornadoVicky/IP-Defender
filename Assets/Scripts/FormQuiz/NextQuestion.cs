using UnityEngine;
using UnityEngine.UI;

public class NextQuestion : MonoBehaviour
{
    public QuizController[] quizControllers; // Assign this array in the Unity Editor
    public int activationNumber = 1; // Start from 1
    private int numberOfQuizzes;

    private void Start()
    {
        numberOfQuizzes = quizControllers.Length;
        // Add a click listener to the button
        Button nextButton = GetComponent<Button>();
        nextButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        // Check if the end of the quiz has been reached
        if (activationNumber <= numberOfQuizzes)
        {
            // Loop through all quiz controllers to find the one with the matching activationNumber
            foreach (var quizController in quizControllers)
            {
                if (quizController.order == activationNumber)
                {
                    // Activate the quiz and reset its state
                    quizController.ActivateQuiz();

                    // Enable the Animator of the next quiz (if available)
                    int nextIndex = (activationNumber % numberOfQuizzes) + 1;
                    QuizController nextQuiz = quizControllers[nextIndex - 1];

                    if (nextQuiz != null)
                    {
                        Animator nextAnimator = nextQuiz.GetComponent<Animator>();
                        if (nextAnimator != null)
                        {
                            nextAnimator.enabled = true;
                            activationNumber++;

                            Debug.Log($"Button clicked! Activating quiz {activationNumber}.");
                        }
                        else
                        {
                            Debug.LogWarning($"Animator not found on quiz {nextQuiz.order}.");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("No next quiz found.");
                    }

                    break; // Exit the loop once a match is found
                }
            }

            // Check if the end of the quiz has been reached after incrementing activationNumber
            if (activationNumber >=numberOfQuizzes)
            {
                // Deactivate the next button
                GetComponent<Button>().interactable = false;
                Debug.Log("End of the quiz reached! Next button deactivated.");
            }
        }
        else
        {
            Debug.Log("End of the quiz reached!"); // Add your logic for the end of the quiz
        }
    }
}
