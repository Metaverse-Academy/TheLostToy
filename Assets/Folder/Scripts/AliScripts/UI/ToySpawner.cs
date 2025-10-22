using UnityEngine;

public class ToySpawner : MonoBehaviour
{
    public GameObject dollPrefab;
    public Transform[] spawnPoints;
    public float spawnDelay = 3f;

    private void Start()
    {
        SpawnDoll();
    }

    void SpawnDoll()
    {
        if (spawnPoints.Length == 0 || dollPrefab == null) return;

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform randomSpawn = spawnPoints[randomIndex];

        Instantiate(dollPrefab, randomSpawn.position, randomSpawn.rotation);
    }
}