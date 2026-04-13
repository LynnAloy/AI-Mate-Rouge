using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //Variable
    [SerializeField] private int a;//a stands for log function base
    //Reference
    [SerializeField] protected WeaponSO weaponSO;
    //[SerializeField] private EnemyDamager enemyDamager;

    public int WeaponLevel;
    public Action HasLeveledUp;
    
    private IDamager iDamager;
    protected float attackRange;
    protected float attackSpeed;
    protected float timeBetweenAttack;

    protected virtual void Awake()
    {
        attackRange = weaponSO.AttackRange;
        attackSpeed = weaponSO.AttackSpeed;
        timeBetweenAttack = weaponSO.TimeBetweenAttack;
        iDamager = GetComponentInChildren<IDamager>(true);
        if (iDamager == null)
        {
            Debug.LogError("Weapon: No IDamager component found in children.");
        }
    }

    private void Start()
    {
       
    }

    protected void WeaponLevelUp()
    {
        Debug.Log("Weapon level up.");
        WeaponLevel++;
    }

    public void WeaponLevelUp(int upAmount)
    {
        WeaponLevel += upAmount;
        HasLeveledUp?.Invoke();
        Debug.Log($"Weapon: iDamager in weapon is {iDamager == null}");
        iDamager.OnWeaponLevelUp(WeaponLevel);
    }

    protected float LevelToRatioAttackSpeed(int weaponLevel)
    {
        return Mathf.Log(weaponLevel, a / 2) + 1;
    }

    protected float LevelToRatioAttackRange(int weaponLevel)
    {
        return Mathf.Log(weaponLevel, a) + 1;
    }

    protected float LevelToTimeBetweenAttack(int weaponLevel)
    {
        return Mathf.Log(weaponLevel, a) + 1;
    }
}
