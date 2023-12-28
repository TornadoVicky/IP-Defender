using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public WaypointArray[] waypointArrays; // Array of WaypointArrays
    public GameObject[] enemyPrefabs; // Array of enemy prefabs
    public float instantiationInterval = 1.0f; // Time between instantiations
    public ComboDisplay comboDisplay; // Reference to the ComboDisplay script

    private int currentEnemyPrefabIndex = 0;
    private float timer = 0.0f;

    void Start()
    {
        // Find all game objects with WaypointArray script
        waypointArrays = FindObjectsOfType<WaypointArray>();

        // Start spawning enemies at intervals
        InvokeRepeating("SpawnNextEnemy", 0f, instantiationInterval);
    }

    private void SpawnNextEnemy()
    {
        if (currentEnemyPrefabIndex >= enemyPrefabs.Length)
        {
            // All enemy types have been spawned
            CancelInvoke("SpawnNextEnemy");
            return;
        }

        int randomWaypointArrayIndex = Random.Range(0, waypointArrays.Length);

        GameObject newEnemy = Instantiate(enemyPrefabs[currentEnemyPrefabIndex], transform.position, Quaternion.identity);
        FollowThePath enemyScript = newEnemy.GetComponent<FollowThePath>();

        if (enemyScript != null && randomWaypointArrayIndex < waypointArrays.Length)
        {
            enemyScript.waypoints = waypointArrays[randomWaypointArrayIndex].waypoints;
        }

        currentEnemyPrefabIndex++;
        EnemyCollision enemyScript1 = newEnemy.GetComponent<EnemyCollision>();
        enemyScript1.comboDisplay = comboDisplay;

    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= instantiationInterval)
        {
            timer = 0.0f;
            SpawnNextEnemy();
        }
    }
}