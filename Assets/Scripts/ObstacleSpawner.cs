using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Spawning Settings")]
    public GameObject obstaclePrefab; // Assign the Square Prefab in the Inspector
    public float spawnRate = 2f; // Spawn every 2 seconds
    public float obstacleSpeed = 5f; // Movement speed of obstacles

    [Header("Lane Positions")]
    public float[] laneYPositions = new float[3] { -1.0f, -2.5f, -4.0f }; // Match player lanes

    void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            // Select a random lane
            int laneIndex = Random.Range(0, laneYPositions.Length);
            float laneY = laneYPositions[laneIndex];

            // Spawn the obstacle
            Vector3 spawnPosition = new Vector3(transform.position.x, laneYPositions[laneIndex] - 1.5f, 0);

            print($"🚀 Spawning obstacle at Y: {laneY} (Before Offset), {spawnPosition.y} (After Offset)");

            GameObject newObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);

            // Move it left towards the player
            newObstacle.GetComponent<Rigidbody2D>().linearVelocity = Vector2.left * obstacleSpeed;

            // Destroy after 5 seconds
            Destroy(newObstacle, 5f);
        }
    }
}
