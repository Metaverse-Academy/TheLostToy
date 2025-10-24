using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] toolPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int maxSpawns = 5;

    private void Start()
    {
        SpawnRandomTools();
    }
    
    private void SpawnRandomTools()
    {
        ClearSpawnedTools();
        
        for (int i = 0; i < maxSpawns && i < spawnPoints.Length; i++)
        {
            if (toolPrefabs.Length > 0)
            {
                int randomToolIndex = Random.Range(0, toolPrefabs.Length);
                int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
                
                Instantiate(toolPrefabs[randomToolIndex], spawnPoints[randomSpawnIndex].position, spawnPoints[randomSpawnIndex].rotation);
            }
        }
    }
    
    private void ClearSpawnedTools()
    {
        GameObject[] spawnedTools = GameObject.FindGameObjectsWithTag("SpawnedTool");
        foreach (GameObject tool in spawnedTools)
        {
            DestroyImmediate(tool);
        }
    }
    
    public void RespawnTools()
    {
        SpawnRandomTools();
    }
}
