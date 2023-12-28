using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public GameObject[] optionContainers; // Array to store option containers for each question
    public Animator[] animators; // Array to store animators
    public Button nextButton; // Reference to the button

    public string[] questions;
    public string[][] options; // 2D array to store options for each question
    private int currentQuestionIndex = 0;

    // Typewriter effect variables
    public float typewriterSpeed = 0.05f;
    private Coroutine typewriterCoroutine;

    private void Start()
    {
        // Start the initialization process after a delay
        Invoke("InitializeQuiz", 2.0f);
    }

    private void InitializeQuiz()
    {
        // Initialize the question text and activate the option container for the first question
        StartCoroutine(TypeWriterEffect(questions[currentQuestionIndex]));
    }

    public void NextQuestion()
    {
        // Move to the next question
        currentQuestionIndex++;

        // Check if we have reached the end of questions
        if (currentQuestionIndex < questions.Length)
        {
            // Deactivate the current option container
            DeactivateOptionContainer(currentQuestionIndex - 1);

            // Update the question text with typewriter effect
            StartCoroutine(TypeWriterEffect(questions[currentQuestionIndex]));

            // Deactivate the next button
            nextButton.interactable = false;
        }
        else
        {
            // If no more questions, you can handle the end of the quiz here
            Debug.Log("End of Quiz");
        }

        // Trigger "Next" parameter in all animators
        foreach (var animator in animators)
        {
            animator.SetTrigger("Next");
        }
    }

    private IEnumerator TypeWriterEffect(string text)
    {
        questionText.text = ""; // Clear existing text

        // Calculate the duration based on text length and typewriter speed
        float duration = text.Length * typewriterSpeed;

        // Display the text one character at a time
        for (int i = 0; i < text.Length; i++)
        {
            questionText.text += text[i];
            yield return new WaitForSeconds(typewriterSpeed);
        }

        // Wait for a moment after finishing the text
        yield return new WaitForSeconds(duration * 0.2f); // Adjust the multiplier as needed

        // Activate the option container for the current question
        ActivateOptionContainer(currentQuestionIndex);

        // Trigger "Options" parameter in all animators
        foreach (var animator in animators)
        {
            animator.SetTrigger("Options");
        }
    }

    private void ActivateOptionContainer(int index)
    {
        // Deactivate all option containers
        for (int i = 0; i < optionContainers.Length; i++)
        {
            optionContainers[i].SetActive(false);
        }

        // Activate the option container for the current question
        if (index < optionContainers.Length)
        {
            optionContainers[index].SetActive(true);
        }
        else
        {
            Debug.LogError("Not enough option containers for questions.");
        }
    }

    private void DeactivateOptionContainer(int index)
    {
        // Deactivate the option container for the specified question
        if (index >= 0 && index < optionContainers.Length)
        {
            optionContainers[index].SetActive(false);
        }
    }
}
