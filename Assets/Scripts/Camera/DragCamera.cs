using UnityEngine;

public class DragCamera : MonoBehaviour
{
    private Vector3 dragOrigin;
    private Vector3 targetPosition;
    public float sensitivity = 0.01f;
    public float smoothness = 5f;
    public float minCameraX = -10f; // Minimum X position
    public float maxCameraX = 10f;  // Maximum X position
    public float minCameraY = -10f; // Minimum Y position
    public float maxCameraY = 10f;  // Maximum Y position
    public float minZoom = 2f;  // Minimum orthographic size
    public float maxZoom = 10f; // Maximum orthographic size

    public float zoomModifierSpeed = 0.1f;

    private Camera mainCamera;
    private float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;
    private Vector2 firstTouchPrevPos, secondTouchPrevPos;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        // Dragging logic
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                dragOrigin = new Vector3(touch.position.x, touch.position.y, 0);
                targetPosition = transform.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 touchPosition = new Vector3(touch.position.x, touch.position.y, 0);
                Vector3 difference = (dragOrigin - touchPosition) * sensitivity;
                dragOrigin = touchPosition;

                targetPosition += difference;

                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothness);
            }

            // Ensure the camera stays within the boundaries
            targetPosition.x = Mathf.Clamp(targetPosition.x, minCameraX, maxCameraX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minCameraY, maxCameraY);
        }

        // Zooming logic
        if (Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

            zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;

            if (touchesPrevPosDifference > touchesCurPosDifference)
                mainCamera.orthographicSize += zoomModifier;
            if (touchesPrevPosDifference < touchesCurPosDifference)
                mainCamera.orthographicSize -= zoomModifier;

            // Apply zoom limits
            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minZoom, maxZoom);
        }
    }

    void OnDrawGizmos()
    {
        // Draw a rectangle to represent the allowed area
        Gizmos.color = Color.green;
        Vector3 minCorner = new Vector3(minCameraX, minCameraY, 0);
        Vector3 maxCorner = new Vector3(maxCameraX, maxCameraY, 0);

        Gizmos.DrawLine(minCorner, new Vector3(maxCorner.x, minCorner.y, 0));
        Gizmos.DrawLine(minCorner, new Vector3(minCorner.x, maxCorner.y, 0));
        Gizmos.DrawLine(maxCorner, new Vector3(maxCorner.x, minCorner.y, 0));
        Gizmos.DrawLine(maxCorner, new Vector3(minCorner.x, maxCorner.y, 0));
    }
}
