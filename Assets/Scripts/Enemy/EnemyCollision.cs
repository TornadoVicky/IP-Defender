using UnityEngine;
using System.Collections;

public class EnemyCollision : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    public float delayBeforeDestroy = 1.0f;
    public int currencyReward = 1;
    public int bullet1Dmg = 1;
    public int bullet2Dmg = 1;
    public int bullet3Dmg = 1;
    public int bullet4Dmg = 1;
    public ComboDisplay comboDisplay; // Reference to the ComboDisplay script

    // Reference to the FollowThePath script
    private FollowThePath followScript;

    public float gizmoRadius = 2.0f; // Adjust the radius of the circular gizmo

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        // Get the FollowThePath script from the same GameObject
        followScript = GetComponent<FollowThePath>();
    }

    void Update()
    {
        // Assuming that the Gizmo is a circular area centered around the enemy,
        // you can check if the enemy is within that area and apply damage.
        // Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, gizmoRadius);

        // foreach (Collider2D collider in colliders)
        // {
        //     if (collider.CompareTag("HighlightCircle"))
        //     {
        //         TakeDamage(1);
        //         break; // Exit the loop after damaging once to avoid multiple damage in one frame.
        //     }
        // }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet1"))
        {
            TakeDamage(bullet1Dmg);
        }
        else if (collision.gameObject.CompareTag("Bullet2"))
        {
            TakeDamage(bullet2Dmg);
        }
        else if (collision.gameObject.CompareTag("Bullet3"))
        {
            TakeDamage(bullet3Dmg);
        }
        else if (collision.gameObject.CompareTag("Bullet4"))
        {
            TakeDamage(bullet4Dmg);
        }
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Set moveSpeed in FollowThePath script to 0
        followScript.moveSpeed = 0;

        animator.SetBool("isDead", true);

        CurrencyManager currencyManager = FindObjectOfType<CurrencyManager>();
        if (currencyManager != null)
        {
            currencyManager.AddCurrency(currencyReward);
        }

        if (comboDisplay != null)
        {
            comboDisplay.IncreaseCombo();
        }

        StartCoroutine(DestroyAfterDelay());
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeDestroy);
        Destroy(gameObject);
    }
}