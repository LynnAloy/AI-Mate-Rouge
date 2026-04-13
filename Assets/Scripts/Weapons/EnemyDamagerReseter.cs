using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyDamagerPair
{
    public DamagerSO BaseDamager;
    public DamagerSO RuntimeDamager;
}

public class EnemyDamagerReseter : Singleton<EnemyDamagerReseter>
{
    [SerializeField] private List<EnemyDamagerPair> damagerPairs;

    protected override void Awake()
    {
        foreach(var damagerPair in damagerPairs)
        {
            damagerPair.RuntimeDamager.DamageAmount = damagerPair.BaseDamager.DamageAmount;
            damagerPair.RuntimeDamager.DamagerSize = damagerPair.BaseDamager.DamagerSize;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
