using UnityEngine;
using UnityEngine.UI;

public class OptionContainer : MonoBehaviour
{
    public Button[] optionButtons; // Assign your option buttons in the inspector
    public int correctOptionIndex = 0; // Index of the correct option
    public Animator[] animators; // Array to store animators
    public Button nextButton; // Reference to the NextButton
    public float delayBeforeNextButtonActivation = 1.0f; // Customizable delay
    private int selectedOptionIndex;

    public float delayBeforeCheckingAnswer = 1.0f; // Adjust this delay as needed

    private void Start()
    {
        // Assign click listeners to option buttons
        for (int i = 0; i < optionButtons.Length; i++)
        {
            int index = i; // Create a local variable to capture the current value of i
            optionButtons[i].onClick.AddListener(() => OnOptionSelected(index));
        }
    }

    private void OnOptionSelected(int selectedOptionIndex)
    {
        // Disable all option buttons to prevent further selections
        foreach (var button in optionButtons)
        {
            button.interactable = false;
        }

        // Activate the NextButton after a customizable delay
        Invoke("ActivateNextButton", delayBeforeNextButtonActivation);

        // Store the selected option index for later use
        this.selectedOptionIndex = selectedOptionIndex;

        // Trigger "Chosen" parameter in all animators
        foreach (var animator in animators)
        {
            animator.SetTrigger("Chosen");
        }

        // After a delay, check if the option is correct
        Invoke("CheckAnswer", delayBeforeCheckingAnswer);
    }

    private void ActivateNextButton()
    {
        // Activate the NextButton
        nextButton.interactable = true;
    }

    private void CheckAnswer()
    {
        // Check if the selected option is correct
        if (selectedOptionIndex == correctOptionIndex)
        {
            Debug.Log("Correct Option Selected!");
            // Handle correct answer logic here

            // Set "isCorrect" parameter to true and trigger "Spoken" parameter
            foreach (var animator in animators)
            {
                animator.SetBool("isCorrect", true);
                animator.SetTrigger("Spoken");
            }
        }
        else
        {
            Debug.Log("Incorrect Option Selected!");
            // Handle incorrect answer logic here

            // Set "isCorrect" parameter to false and trigger "Spoken" parameter
            foreach (var animator in animators)
            {
                animator.SetBool("isCorrect", false);
                animator.SetTrigger("Spoken");
            }
        }
    }
}
