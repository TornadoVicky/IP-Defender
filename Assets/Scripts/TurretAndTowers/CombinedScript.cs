using UnityEngine;

public class CombinedScript : MonoBehaviour
{
    // Projectile Launcher Variables
    public GameObject projectilePrefab; // The projectile prefab
    public float fireRate = 1.0f; // The rate of fire in shots per second
    public float projectileSpeed = 10.0f; // The speed of the projectile
    public float angleOffset = 0.0f; // Additional angle offset for launch direction
    private float lastFireTime;

    // Turret Variables
    public float range = 10f; // The range of the turret
    public float rotationSpeed = 5f; // The speed at which the turret rotates
    public float turretAngleOffset = 0f; // Additional angle offset for the turret

    // Cost Definition Variables
    public int cost = 10; // Define the cost for this object

    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Set it to not play on awake to prevent any accidental audio
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
        }
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
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + turretAngleOffset;
            Quaternion lookRotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // Debug information
            Debug.Log("Direction: " + direction);

            // Rotate towards the tap position
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

            // Fire projectiles here (you may call a method in ProjectileLauncher)
            FireProjectile();
        }
        else
        {
            // No tap, return to the original rotation
            // transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Calculate the launch direction based on the angle offset
        Vector2 launchDirection = Quaternion.Euler(0, 0, angleOffset) * transform.up;

        // Set the velocity of the projectile based on the launch direction and speed
        rb.velocity = launchDirection.normalized * projectileSpeed;
    }

    // Check if the tap is within the defined detection area
    bool IsTapWithinDetectionArea()
    {
        Vector3 tapPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector3.Distance(transform.position, tapPosition);
        return distance <= range;
    }

    // Cost Definition Methods
    public int GetCost()
    {
        return cost;
    }
}
