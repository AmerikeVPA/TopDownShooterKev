using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EnemyManager enemyManager;
    public HealthSystem playerHealth;
    public UIController uiController;

    private int _wave = 0, score = 0;

    private void Awake()
    {
        CommenceWave();
    }
    public void CommenceWave()
    {
        Time.timeScale = 1.0f;
        enemyManager.StartWave();
        uiController.ResumeGame();
    }
    public void EndWave()
    {
        _wave++;
        enemyManager._difficultyMultiplier += 0.1f;
        enemyManager.initialWaveEnemies++;
        Time.timeScale = 0.0f;
        uiController.ShowStore();
    }
    public void AddScore(int scroeToAdd)
    {
        score += scroeToAdd;
    }
    public void GameOver()
    {
        Time.timeScale = 0.0f;
        uiController.ShowGameOver(score);
    }
}
