using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Transform obstaclesContainer;
    public Transform player;
    public GameObject obstaclePrefab;

    public float spawnInterval = 2f;
    public float spawnZ = 10f;
    public float laneDistance = 3f;
    public float spawnY = 70.5f;

    float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        Debug.Log("ObstacleSpawner running");

        if (player == null || obstaclePrefab == null)
            return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnObstacle();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnObstacle()
    {
        float centerX = player.position.x;

        int laneOffset = Random.Range(-1, 2); // -1 left, 0 middle, 1 right
        float laneX = centerX + laneOffset * laneDistance;

        Vector3 spawnPosition = new Vector3(
            laneX,
            spawnY,
            player.position.z - spawnZ
        );

        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity,obstaclesContainer);
    }
}
