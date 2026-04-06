using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : Singleton<WaveController>
{
    public Action OnWaveEnd;

    [SerializeField] private EnemySpawner enemySpawner;

    private float waveLength;
    private float waveCounter;
    private int currentWave = 1;
    private bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        waveLength = DifficultyController.Instance.GetWaveLength(currentWave);
        waveCounter = waveLength;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused) return;
        waveCounter -= Time.deltaTime;
        if(waveCounter <= 0)
        {
            EndWave();
        }
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }

    private void EndWave()
    {
        isPaused = true;
        DestroyAllEnemies();
        if(enemySpawner != null)
        {
            enemySpawner.ResetSpawnerInfo();
        }
        OnWaveEnd?.Invoke();
        Time.timeScale = 0f;
        GameObject panel = UpGradeUIController.Instance.GetUpGradeUIPanel();
        panel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        currentWave++;
        waveLength = DifficultyController.Instance.GetWaveLength(currentWave);
        waveCounter = waveLength;
    }

    private void DestroyAllEnemies()
    {
        var all = FindObjectsOfType<EnemyController>();
        foreach (EnemyController enemy in all)
        {
            if(enemy != null && enemy.gameObject != null)
            {
                Destroy(enemy.gameObject);
            }
        }
    }
}
