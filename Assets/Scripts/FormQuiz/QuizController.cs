using UnityEngine;

public class QuizController : MonoBehaviour
{
    public int order; // Assign this in the Unity Editor

    private Animator quizAnimator;

    void Start()
    {
        quizAnimator = GetComponent<Animator>();
    }

    public void ActivateQuiz()
    {
        // Reset the state of the quiz to its initial state
        quizAnimator.Play("DefaultState", 0, 0f);

        // Activate the toggle trigger in the Animator
        quizAnimator.SetTrigger("Next");

        // Optionally, you can perform additional actions or logic here
        Debug.Log($"Quiz {order} activated!");
    }
}
