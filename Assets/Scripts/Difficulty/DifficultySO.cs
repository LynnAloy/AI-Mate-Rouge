using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Difficulty
{
    //f(x) = a + b / (1 + e ^ (c * (x - d)))
    public int WaveLength_a;//8
    public int WaveLength_b;//17
    public float WaveLength_c;//-0.25
    public int WaveLength_d;//20
    public int SpawnLimitLevel_a;//1
    public int SpawnLimitLevel_b;//5
    public int SpawnLimit_a;//2
    public int SpawnLimit_b;//3
    //f(x) = a + (b - a) / (1 + e ^ (c * (x - d)))
    public int WaveInterval_a;//1
    public int WaveInterval_b;//4
    public float WaveInterval_c;//0.25
    public int WaveInterval_d;//20
}

[CreateAssetMenu(menuName = "Difficulty")]
public class DifficultySO : ScriptableObject
{
    //数据结构说明 {难度：{刷怪数量上线，刷怪等级上线}}
    [SerializeField] List<Difficulty> difficulties;

    public List<Difficulty> Difficulties => difficulties;

}
