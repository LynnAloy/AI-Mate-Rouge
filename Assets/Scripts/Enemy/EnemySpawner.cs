using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    //References
    [SerializeField] private Transform leftLowTrans;
    [SerializeField] private Transform rightUpTrans;
    [SerializeField] private Transform playerTrans;
    [SerializeField] private GameObject spawnAlertImage; 
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private List<EnemyController> enemies;
    //Variable
    [SerializeField] private float alertTime;
    //Variable from DifficultySO
    private int spawnLimit;
    private int spawnLimitLevel;
    private float waveLength;
    private float waveInterval;

    private float spawnCounter;//When a wave starts, how much time will spawn a bunch of enemies?
    private float waveCounter;//The whole length of a wave
    private Coroutine alertImageCoroutine;
    private Vector3 spawnPos;
    private bool hasAlertImageDisplayed;
    private List<EnemyController> tempEnemies;

    // Start is called before the first frame update
    void Start()
    {
        spawnAlertImage.SetActive(false);
        spawnPos = GetRandomRespawnPos();
        hasAlertImageDisplayed = false;
        //Get the difficulties from the DifficultySO
        //hard code here should be difficulties[index] index set by DifficultyManager
        InitSpawnerInfo();
        tempEnemies = new();
    }



    // Update is called once per frame
    void Update()
    {
        if (PlayerHealthController.Instance.gameObject.activeSelf)
        {
            if (waveCounter > 0)
            {
                waveCounter -= Time.deltaTime;
                spawnCounter -= Time.deltaTime;
                if (spawnCounter <= alertTime && !hasAlertImageDisplayed)
                {
                    StartAlertImage(spawnPos);
                    hasAlertImageDisplayed = true;
                }
                if (spawnCounter <= 0)
                {
                    spawnCounter = waveInterval;
                    SpawnEnemy(spawnLimit, spawnLimitLevel);
                    //Create a random enemy respawn position
                    spawnPos = GetRandomRespawnPos();
                    hasAlertImageDisplayed = false;
                }
            }
            else
            {
                waveCounter = waveLength;
            }
        }
    }

    private void InitSpawnerInfo()
    {
        int currentWave = WaveController.Instance.GetCurrentWave();
        spawnLimit = DifficultyController.Instance.GetSpawnLimit(currentWave);
        spawnLimitLevel = DifficultyController.Instance.GetSpawnLimitLevel(currentWave);
        waveLength = DifficultyController.Instance.GetWaveLength(currentWave);
        waveInterval = DifficultyController.Instance.GetWaveInterval(currentWave);
        spawnCounter = waveInterval;
        waveCounter = waveLength;   
        spawnPos = GetRandomRespawnPos();
    }

    public void ResetSpawnerInfo()
    {
        tempEnemies.Clear();
        hasAlertImageDisplayed = false;
        spawnPos = GetRandomRespawnPos();
        InitSpawnerInfo();
    }

    private void SpawnEnemy(int amountlimit, int levellimit)
    {
        if(tempEnemies.Count == 0)
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                if(enemies[i].GetEnemyLevel() <= levellimit)
                {
                    tempEnemies.Add(enemies[i]);
                }
            }
        }
        if(tempEnemies.Count > 0)
        {
            int randomAmount = Random.Range(1, amountlimit + 1);
            for(int i = 0; i < randomAmount; i++)
            {
                int randomEnemyIndex = Random.Range(0, tempEnemies.Count);
                Instantiate(tempEnemies[randomEnemyIndex], spawnPos, Quaternion.identity);
            }
        }
    }

    private Vector3 GetRandomRespawnPos()
    {
        float randomX = Random.Range(leftLowTrans.position.x, rightUpTrans.position.x);
        float randomY = Random.Range(leftLowTrans.position.y, rightUpTrans.position.y);
        Vector3 spawnVec3 = new(randomX, randomY, 0);
        return spawnVec3;
    }

    private void StartAlertImage(Vector3 vec3)
    {
        if(alertImageCoroutine != null)
        {
            StopCoroutine(alertImageCoroutine);
        }
        alertImageCoroutine = StartCoroutine(DisplayAlertImage(vec3));
    }

    private IEnumerator DisplayAlertImage(Vector3 vec3)
    {
        spawnAlertImage.transform.position = vec3;
        spawnAlertImage.SetActive(true);
        yield return new WaitForSeconds(alertTime);
        spawnAlertImage.SetActive(false);
    }
}
