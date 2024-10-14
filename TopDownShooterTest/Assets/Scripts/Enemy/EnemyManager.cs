using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int poolSize, initialWaveEnemies;
    public float spawnTimer;
    public Transform player;
    public GameObject enemy, coin;
    public GameManager gameManager;

    [HideInInspector]
    public bool _keepSpawning = true;
    private int _waveEnemies, _enemiesSpawned;
    [HideInInspector]
    public float _difficultyMultiplier = 1.0f;
    private Queue<GameObject> enemies = new Queue<GameObject>();

    private void Awake()
    {
        CreateEnemyPool();
    }
    private void CreateEnemyPool()
    {
        for(int i = 0; i < poolSize; i++)
        {
            GameObject newEnemy = Instantiate(enemy, transform);
            newEnemy.name = $"Enemy_{i}";
            newEnemy.GetComponent<Enemy>()._enemyManager = this;
            newEnemy.SetActive(false);
            enemies.Enqueue(newEnemy);
        }
    }
    public void StartWave()
    {
        _waveEnemies = initialWaveEnemies;
        _enemiesSpawned = initialWaveEnemies;
        StartCoroutine(SpawnEnemy());
    }
    private IEnumerator SpawnEnemy()
    {
        if(_waveEnemies == 0) { StopCoroutine(SpawnEnemy()); }
        yield return new WaitForSeconds(spawnTimer);
        if (enemies.Count > 0)
        {
            GameObject nextEnemy = enemies.Dequeue();
            nextEnemy.transform.position = transform.position;
            nextEnemy.SetActive(true);
            _enemiesSpawned--;
        }
        if(_enemiesSpawned > 0) { StartCoroutine(SpawnEnemy()); }
    }
    public void EnemyDestroyed(GameObject enemyDestroyed)
    {
        _waveEnemies--;
        enemies.Enqueue(enemyDestroyed);
        enemyDestroyed.SetActive(false);
        gameManager.AddScore(100);
        if(_waveEnemies == 0) { gameManager.EndWave(); }
    }
}
