using UnityEngine;

public class PanelMovement : MonoBehaviour
{
    public Transform[] targetPositions; // Array of target positions
    public float moveSpeed = 2f; // Speed of movement

    private int currentTargetIndex = 0;
    private bool isMoving = false;

    void Update()
    {
        if (isMoving)
        {
            // Lerp towards the current target position
            transform.position = Vector3.Lerp(transform.position, targetPositions[currentTargetIndex].position, moveSpeed * Time.deltaTime);

            // Check if we are close enough to the target position
            if (Vector3.Distance(transform.position, targetPositions[currentTargetIndex].position) < 0.1f)
            {
                isMoving = false;
                Debug.Log("Arrived at target position.");
            }
        }
    }

    public void MoveToNextTarget()
    {
        if (currentTargetIndex < targetPositions.Length - 1)
        {
            currentTargetIndex++;
            isMoving = true;
            Debug.Log("Moving to next target.");
        }
    }

    public void MoveToPreviousTarget()
    {
        if (currentTargetIndex > 0)
        {
            currentTargetIndex--;
            isMoving = true;
            Debug.Log("Moving to previous target.");
        }
    }

    public void ActivateFromButtonClickHandler()
    {
        Debug.Log("Activated from ButtonClickHandler.");
    }
}
