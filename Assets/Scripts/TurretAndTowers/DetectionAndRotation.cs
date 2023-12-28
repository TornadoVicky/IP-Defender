using UnityEngine;

public class DetectionAndRotation : MonoBehaviour
{
    public float detectionRange = 5f; // Range of detection
    public float rotationSpeed = 50f; // Rotation speed on the Z-axis
    public Color gizmoColor = Color.yellow; // Color of the detection range gizmo
    public GameObject[] bulletSpawnPoints; // Array of bullet spawn points
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public float bulletSpeed = 10f; // Speed of the bullets
    public float fireRate = 1f; // Rate of fire (bullets per second)

    private Quaternion originalRotation;
    private float fireCooldown = 0f;

    private AudioSource audioSource;

    void Start()
    {
        originalRotation = transform.rotation;

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
        // Detect nearby "Enemy" tagged game objects
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        bool enemyDetected = false;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                enemyDetected = true;
                break;
            }
        }

        if (enemyDetected)
        {
            // Rotate on the Z-axis
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

            // Fire bullets if fire cooldown has passed
            if (Time.time > fireCooldown)
            {
                FireBullets();
                fireCooldown = Time.time + 1f / fireRate;
            }

            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // Return to original rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, Time.deltaTime);

            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    void FireBullets()
    {
        foreach (GameObject spawnPoint in bulletSpawnPoints)
        {
            // Spawn a bullet at the spawn point
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = spawnPoint.transform.up * bulletSpeed;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a gizmo to visualize the detection range
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
