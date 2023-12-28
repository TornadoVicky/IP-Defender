using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Add this line for scene management

public class CompareVariableButton : MonoBehaviour
{
    public Button compareButton; // Reference to the compare button
    public TargetObject targetObject1; // Reference to the first target object
    public TargetObject targetObject2; // Reference to the second target object

    // Define multiple predefined pairs of variable values for comparison
    [System.Serializable]
    public struct VariablePair
    {
        public int value1;
        public int value2;
    }

    public VariablePair[] predefinedPairs;

    // References to the two animators
    public Animator mismatchAnimator;
    public Animator matchAnimator;

    // Public delay before disabling the animators
    public float disableDelay = 2f;

    // Scene name to load on mismatch
    public int SceneToLoad = 0;

    void Start()
    {
        // Ensure that the compare button, target objects, and animators are set in the Inspector
        if (compareButton == null)
        {
            Debug.LogError("Compare Button is not set in the inspector!");
        }

        if (targetObject1 == null)
        {
            Debug.LogError("Target Object 1 is not set in the inspector!");
        }

        if (targetObject2 == null)
        {
            Debug.LogError("Target Object 2 is not set in the inspector!");
        }

        if (mismatchAnimator == null)
        {
            Debug.LogError("Mismatch Animator is not set in the inspector!");
        }

        if (matchAnimator == null)
        {
            Debug.LogError("Match Animator is not set in the inspector!");
        }

        // Attach the Click event listener to the compare button
        compareButton.onClick.AddListener(CompareVariableValues);
    }

    void CompareVariableValues()
    {
        // Compare variable values of the two target objects
        int variable1 = targetObject1 != null ? targetObject1.customVariable : 0;
        int variable2 = targetObject2 != null ? targetObject2.customVariable : 0;

        // Compare against multiple predefined pairs
        foreach (var pair in predefinedPairs)
        {
            if (variable1 == pair.value1 && variable2 == pair.value2)
            {
                Debug.Log("Variables match the predefined pair: " + variable1 + ", " + variable2);

                // Enable and activate the match animator
                if (matchAnimator != null)
                {
                    matchAnimator.enabled = true;
                    //matchAnimator.SetTrigger("Activate");
                }

                // Disable animators after a delay
                DisableAnimatorsDelayed();
                return; // Stop comparing once a match is found
            }
        }

        // Log a message if no match is found
        Debug.Log("Variables do not match any predefined pair: " + variable1 + ", " + variable2);

        // Enable and activate the mismatch animator
        if (mismatchAnimator != null)
        {
            mismatchAnimator.enabled = true;
            //mismatchAnimator.SetTrigger("Activate");

            // Load the mismatch scene after a delay
            Invoke("LoadMismatchScene", disableDelay);
        }
    }

    // Disable animators after a delay
    void DisableAnimatorsDelayed()
    {
        Invoke("DisableAnimators", disableDelay);
    }

    // Disable the animators
    void DisableAnimators()
    {
        if (mismatchAnimator != null)
        {
            mismatchAnimator.enabled = false;
        }

        if (matchAnimator != null)
        {
            matchAnimator.enabled = false;
        }
    }

    // Load the mismatch scene
    void LoadMismatchScene()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
}
