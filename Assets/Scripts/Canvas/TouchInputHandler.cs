using UnityEngine;

public class TouchInputHandler : MonoBehaviour
{
    public int canvasIndex; // Unique identifier for the canvas associated with this object

    void Update()
    {
        // Check if there is any touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check if the touch position is over this game object
            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                Collider2D hitCollider = Physics2D.OverlapPoint(touchPosition);

                if (hitCollider != null && hitCollider.gameObject == gameObject)
                {
                    // Toggle the canvas through the CanvasManager using the canvasIndex
                    CanvasManager.instance.ToggleCanvas(canvasIndex);
                }
            }
        }
    }
}
