using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int WeaponLevel;
    public Action HasLeveledUp;
    protected float damage;
    [SerializeField] private int a;//a stands for log function base
    [SerializeField] protected WeaponSO weaponSO;
    //For testing leveling weapon

    protected virtual void Awake()
    {
        damage = weaponSO.Damage;
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
    }

    public virtual void SetWeaponDamage(float ratio)
    {
        damage *= ratio;
    }
    
    public float GetWeaponDamage()
    {
        Debug.Log($"Weapon: Damage - {damage}");
        return damage;
    }


    protected float LevelToRatioDamage(int weaponLevel)
    {
        return Mathf.Log(weaponLevel, a) + 1;
    }

    protected float LevelToRatioDuration(int weaponLevel)
    {
        return Mathf.Log(weaponLevel, a) + 1;
    }

    protected float LevelToRatioAttackSpeed(int weaponLevel)
    {
        return Mathf.Log(weaponLevel, a / 2) + 1;
    }

    protected float LevelToRatioAttackRange(int weaponLevel)
    {
        return Mathf.Log(weaponLevel, a) + 1;
    }
}
