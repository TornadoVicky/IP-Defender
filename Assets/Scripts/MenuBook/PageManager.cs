using UnityEngine;

public class PageManager : MonoBehaviour
{
    public GameObject[] targetObjects; // Array of target GameObjects to activate
    private int currentPageIndex = 0; // Index of the currently active page

    public float activationDelay = 1.0f; // Delay before activating the next page

    private bool isBookOpen = false; // Flag indicating whether the book is open

    void Start()
    {
        // Ensure valid references are set in the Inspector
        InitializePages();
    }

    void InitializePages()
    {
        for (int i = 0; i < targetObjects.Length; i++)
        {
            if (targetObjects[i] != null)
            {
                // Deactivate all pages initially
                targetObjects[i].SetActive(false);
            }
            else
            {
                Debug.LogError("One of the target pages is not assigned!");
            }
        }
    }

    public void FlipForward()
    {
        // Deactivate the currently active page immediately
        targetObjects[currentPageIndex].SetActive(false);

        // Move to the next page
        currentPageIndex = (currentPageIndex + 1) % targetObjects.Length;

        // Activate the next page after a delay if the book is open
        if (isBookOpen)
        {
            Invoke("ActivateNextPage", activationDelay);
        }
    }

    public void FlipBackward()
    {
        // Deactivate the currently active page immediately
        targetObjects[currentPageIndex].SetActive(false);

        // Move to the previous page
        currentPageIndex = (currentPageIndex - 1 + targetObjects.Length) % targetObjects.Length;

        // Activate the previous page after a delay if the book is open
        if (isBookOpen)
        {
            Invoke("ActivatePreviousPage", activationDelay);
        }
    }

    void ActivateNextPage()
    {
        // Activate the next page
        targetObjects[currentPageIndex].SetActive(true);
    }

    void ActivatePreviousPage()
    {
        // Activate the previous page
        targetObjects[currentPageIndex].SetActive(true);
    }

    public void SetBookOpenState(bool isOpen)
    {
        // Set the book open state
        isBookOpen = isOpen;

        // Log current book state
        Debug.Log("Book is now " + (isBookOpen ? "open" : "closed"));

        // Deactivate all target objects if the book is closed
        if (!isBookOpen)
        {
            foreach (var targetObject in targetObjects)
            {
                if (targetObject != null)
                {
                    targetObject.SetActive(false);
                }
                else
                {
                    Debug.LogError("One of the target objects is not assigned!");
                }
            }
        }
    }
}
