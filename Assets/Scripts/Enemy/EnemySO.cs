using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    [SerializeField] private Sprite enemySprite;
    [SerializeField] private float enemyHealth;
    [SerializeField] private float enemyDamage;
    [SerializeField] private float enemyMinMoveSpeed;
    [SerializeField] private float enemyMaxMoveSpeed;
    [SerializeField] private AnimationClip idle;
    [SerializeField] private AnimationClip move;
    [SerializeField] private int enemyLevel;
    [SerializeField] private int expToDrop;


    public Sprite EnemySprite => enemySprite;
    public float EnemyHealth => enemyHealth;
    public float EnemyDamage => enemyDamage;
    public AnimationClip Idle => idle;
    public AnimationClip Move => move;
    public float EnemyMinMoveSpeed => enemyMinMoveSpeed;
    public float EnemyMaxMoveSpeed => enemyMaxMoveSpeed;
    public int EnemyLevel => enemyLevel;
    public int ExpToDrop => expToDrop;
}
