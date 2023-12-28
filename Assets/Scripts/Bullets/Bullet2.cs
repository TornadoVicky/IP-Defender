using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    public float destroyTime = 2f; // Time in seconds before the bullet is destroyed automatically

    void Start()
    {
        // Automatically destroy the bullet after destroyTime seconds
        Destroy(gameObject, destroyTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the bullet upon collision with an object tagged as "Enemy"
            Destroy(gameObject);
        }
    }
}
