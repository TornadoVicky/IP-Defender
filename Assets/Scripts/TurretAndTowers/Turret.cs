using UnityEngine;

public class Turret : MonoBehaviour
{
    public float range = 10f; // The range of the turret
    public float rotationSpeed = 5f; // The speed at which the turret rotates
    public float angleOffset = 0f; // Additional angle offset

    //private Quaternion originalRotation;

    // Define delegate (if using non-generic pattern).
    public delegate void TargetAcquiredAction();
    public static event TargetAcquiredAction OnTargetAcquired;

    public delegate void TargetLostAction();
    public static event TargetLostAction OnTargetLost;

    private AudioSource audioSource;

    void Start()
    {
        //originalRotation = transform.rotation;

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Set it to not play on awake to prevent any accidental audio
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void Update()
{
    // Check for taps within the defined area
    if (Input.GetMouseButtonDown(0) && IsTapWithinDetectionArea())
    {
        // Get the tap position
        Vector3 tapPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Debug information
        Debug.Log("Tap Position: " + tapPosition);

        // Calculate the direction to the tap position
        Vector3 direction = tapPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + angleOffset;
        Quaternion lookRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Debug information
        Debug.Log("Direction: " + direction);

        // Rotate towards the tap position
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        // Trigger the event for target acquired
        if (OnTargetAcquired != null)
        {
            OnTargetAcquired();
        }

        // Fire projectiles here (you may call a method in ProjectileLauncher)
    }
    else
    {
        // No tap, return to the original rotation
        //transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, rotationSpeed * Time.deltaTime);

        // Trigger the event for target lost
        if (OnTargetLost != null)
        {
            OnTargetLost();
        }
    }
}


    // Check if the tap is within the defined detection area
    bool IsTapWithinDetectionArea()
    {
        Vector3 tapPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector3.Distance(transform.position, tapPosition);
        return distance <= range;
    }
}
