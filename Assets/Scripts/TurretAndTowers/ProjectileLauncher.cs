using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePrefab; // The projectile prefab
    public float fireRate = 1.0f; // The rate of fire in shots per second
    public float projectileSpeed = 10.0f; // The speed of the projectile
    public float angleOffset = 0.0f; // Additional angle offset for launch direction

    private bool canFire = false;
    private float lastFireTime;

    void Start()
    {
        // Enable firing when a target is acquired
        Turret.OnTargetAcquired += EnableFiring;

        // Disable firing when the target is lost
        Turret.OnTargetLost += DisableFiring;
    }

    void Update()
    {
        if (canFire)
        {
            // Check if enough time has passed since the last fire
            if (Time.time - lastFireTime >= 1 / fireRate)
            {
                FireProjectile();
                lastFireTime = Time.time;
            }
        }
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

    void EnableFiring()
    {
        canFire = true;
    }

    void DisableFiring()
    {
        canFire = false;
    }
}
