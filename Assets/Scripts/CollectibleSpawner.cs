using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public GameObject collectiblePrefab;

    [Header("Spawn Settings")]
    public float spawnInterval = 2f;
    public float spawnDistanceZ = 10f;
    public float laneDistance = 3f;
    public float spawnHeightOffset = 0.5f;

    float nextSpawnTime;
    float startX;

    void Start()
    {
        startX = player.position.x;
        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        if (player == null || collectiblePrefab == null)
            return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnCollectible();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnCollectible()
    {
        // Choose random lane
        int laneIndex = Random.Range(0, 3);
        float laneX = startX + (laneIndex - 1) * laneDistance;

        Vector3 spawnPosition = new Vector3(
            laneX,
            player.position.y + spawnHeightOffset,
            player.position.z - spawnDistanceZ
        );

        Instantiate(collectiblePrefab, spawnPosition, Quaternion.identity);
    }
}
