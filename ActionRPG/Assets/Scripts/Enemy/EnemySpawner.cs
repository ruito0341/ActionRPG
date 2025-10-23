using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;
    public Transform parent;
    public Vector2 spawnAreaCenter = Vector2.zero; // 修正: spwan -> spawn
    public Vector2 spawnAreaSize = new Vector2(10f, 5f);
    public float spawnInterval = 2f;
    public int maxEnemies = 10;
    public int initialSpawn = 3;

    private List<GameObject> spawned = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < initialSpawn; i++)
        {
            TrySpawn();
        }
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            TrySpawn();
            CleanupNulls();
        }
    }

    void TrySpawn()
    {
        CleanupNulls();
        if (enemyPrefab == null) return;
        if (spawned.Count >= maxEnemies) return;

        Vector2 pos = GetRandomPosition();
        GameObject go = Instantiate(enemyPrefab, pos, Quaternion.identity, parent);
        spawned.Add(go);
    }

    Vector2 GetRandomPosition()
    {
        float x = spawnAreaCenter.x + Random.Range(-spawnAreaSize.x * 0.5f, spawnAreaSize.x * 0.5f);
        float y = spawnAreaCenter.y + Random.Range(-spawnAreaSize.y * 0.5f, spawnAreaSize.y * 0.5f);
        return new Vector2(x, y);
    }
    void CleanupNulls()
    {
        spawned.RemoveAll(item => item == null);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.25f);
        Vector3 center = new Vector3(spawnAreaCenter.x, spawnAreaCenter.y, 0f);
        Gizmos.DrawCube(center, new Vector3(spawnAreaSize.x, spawnAreaSize.y, 0.1f)); // DrawCube に修正
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, new Vector3(spawnAreaSize.x, spawnAreaSize.y, 0.1f)); // Gizmos に修正
    }
}