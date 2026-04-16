using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Difficulty")]
public class DifficultySO : ScriptableObject
{
    //f(x) = a + b / (1 + e ^ (c * (x - d)))
    [SerializeField] private int waveLength_a;//8
    [SerializeField] private int waveLength_b;//17
    [SerializeField] private float waveLength_c;//-0.25
    [SerializeField] private int waveLength_d;//20
    [SerializeField] private int spawnLimitLevel_a;//1
    [SerializeField] private int spawnLimitLevel_b;//5
    [SerializeField] private int spawnLimit_a;//2
    [SerializeField] private int spawnLimit_b;//3
    //f(x) = a + (b - a) / (1 + e ^ (c * (x - d)))
    [SerializeField] private int waveInterval_a;//1
    [SerializeField] private int waveInterval_b;//4
    [SerializeField] private float waveInterval_c;//0.25
    [SerializeField] private int waveInterval_d;//20
    [SerializeField] private int maxWave;//the max wave player can arrive
    [SerializeField] private int bossWave;//if wave % bossWave == 0, is boss wave
    //数据结构说明 {难度：{刷怪数量上线，刷怪等级上线}}

    public int WaveLength_a => waveLength_a;
    public int WaveLength_b => waveLength_b;
    public float WaveLength_c => waveLength_c;
    public int WaveLength_d => waveLength_d;
    public int SpawnLimitLevel_a => spawnLimitLevel_a;
    public int SpawnLimitLevel_b => spawnLimitLevel_b;
    public int SpawnLimit_a => spawnLimit_a;
    public int SpawnLimit_b => spawnLimit_b;
    public int WaveInterval_a => waveInterval_a;
    public int WaveInterval_b => waveInterval_b;
    public float WaveInterval_c => waveInterval_c;
    public int WaveInterval_d => waveInterval_d;

    public int MaxWave
    {
        get => maxWave;
        set => maxWave = value;
    }

    public int BossWave
    {
        get => bossWave;
        set => bossWave = value;
    }


}
