using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : Singleton<DifficultyController>
{
    [SerializeField] private DifficultySO difficultySO;

    //Variables from DifficultySO
    private int waveLength_a;//8
    private int waveLength_b;//17
    private float waveLength_c;//-0.25
    private int waveLength_d;//20
    private int spawnLimitLevel_a;//1
    private int spawnLimitLevel_b;//5
    private int spawnLimit_a;//2
    private int spawnLimit_b;//3
    private int waveInterval_a;//1
    private int waveInterval_b;//4
    private float waveInterval_c;//0.25
    private int waveInterval_d;//20
    private int maxWave;
    private int bossWave;


    protected override void Awake()
    {
        base.Awake();
        waveLength_a = difficultySO.WaveLength_a;
        waveLength_b = difficultySO.WaveLength_b;
        waveLength_c = difficultySO.WaveLength_c;
        waveLength_d = difficultySO.WaveLength_d;
        spawnLimitLevel_a = difficultySO.SpawnLimitLevel_a;
        spawnLimitLevel_b = difficultySO.SpawnLimitLevel_b;
        spawnLimit_a = difficultySO.SpawnLimit_a;
        spawnLimit_b = difficultySO.SpawnLimit_b;
        waveInterval_a = difficultySO.WaveInterval_a;
        waveInterval_b = difficultySO.WaveInterval_b;
        waveInterval_c = difficultySO.WaveInterval_c;
        waveInterval_d = difficultySO.WaveInterval_d;
        maxWave = difficultySO.MaxWave;
        bossWave = difficultySO.BossWave;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetWaveLength(int wave)
    {
        return WaveLengthFunction(wave);
        
    }

    private float WaveLengthFunction(int wave)
    {
        //f(x) = a + b / (1 + e ^ (c * (x - d)))
        return waveLength_a + waveLength_b / (1 + Mathf.Exp(waveLength_c * (wave - waveLength_d)));
    }

    public int GetSpawnLimitLevel(int wave)
    {
        return SpawnLimitLevelFunction(wave);
    }

    private int SpawnLimitLevelFunction(int wave)
    {
        return (int)(spawnLimitLevel_a + spawnLimitLevel_b / (1 + Mathf.Exp(waveLength_c * (wave - waveLength_d))));
    }

    public int GetSpawnLimit(int wave)
    {
        return SpawnLimitFunction(wave);
    }

    private int SpawnLimitFunction(int wave)
    {
        return (int)(spawnLimit_a + spawnLimit_b / (1 + Mathf.Exp(waveLength_c * (wave - waveLength_d))));
    }

    public float GetWaveInterval(int wave)
    {
        return WaveIntervalFunction(wave);
    }

    private float WaveIntervalFunction(int wave)
    {
        return waveInterval_a + (waveInterval_b - waveInterval_a) / (1 + Mathf.Exp(waveInterval_c * (wave - waveInterval_d)));
    }

    public int GetMaxWave()
    {
        return maxWave;
    }

    public void SetMaxWaveExternal(int value)
    {
        difficultySO.MaxWave = value;
    }

    public int GetBossWave()
    {
        return bossWave;
    }

    public void SetBossWaveExternal(int value)
    {
        difficultySO.BossWave = value;
    }
}
