using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLifetime = 2.0f;
    public float minDistanceBeforeSelfDestruct = 1.0f;

    private float spawnTime;
    private Vector3 spawnPosition;

    void Start()
    {
        spawnTime = Time.time;
        spawnPosition = transform.position;

        Destroy(gameObject, bulletLifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            float distanceTravelled = Vector3.Distance(spawnPosition, transform.position);

            if (distanceTravelled >= minDistanceBeforeSelfDestruct)
            {
                EnemyHit(collision.gameObject);
            }
        }
    }

    void EnemyHit(GameObject enemy)
    {
        Destroy(gameObject);
    }
}
