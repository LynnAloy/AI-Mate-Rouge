using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpinWeapon : Weapon
{
    [SerializeField] private float spawnInterval;
    [SerializeField] private Transform holder, fireballToSpawn;
    //Variables from weaponSO
    private float attackSpeed;
    private float attackRange;
    private float timeBetweenAttack;
    private int amount;
    //References
    [SerializeField] private EnemyDamager damager;

    private float spawnTimer;

    protected override void Awake()
    {
        base.Awake();
        spawnTimer = spawnInterval;
        if(damager == null)
        {
            Debug.LogError("SpinWeapon: EnemyDamager reference not set.");
        }
    }

    private void Start()
    {
        InitWeapon();
        HasLeveledUp += WeaponLevelUp;
        HasLeveledUp += SetSpinWeapon;
        HasLeveledUp += SetDamager;
    }

    private void Update()
    {
        holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + attackSpeed * Time.deltaTime * LevelToRatioAttackSpeed(WeaponLevel));
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0f)
        {
            spawnTimer = spawnInterval;
            Instantiate(fireballToSpawn, fireballToSpawn.position, fireballToSpawn.rotation, holder).gameObject.SetActive(true);
        }
    }

    private void InitWeapon()
    {
        attackSpeed = weaponSO.AttackSpeed;
        timeBetweenAttack = weaponSO.TimeBetweenAttack;
        amount = weaponSO.Amount;
        attackRange = weaponSO.AttackRange;
        transform.localScale = Vector3.one * attackRange;
    }

    private void SetSpinWeapon()
    {
        attackRange = LevelToRatioAttackRange(WeaponLevel);
        Debug.Log($"SpinWeapon: AttackRange updated to {attackRange}.");
    }

    
    private void SetDamager()
    {
        damager.SetDamage(LevelToRatioDamage(WeaponLevel));
        damager.SetDuration(LevelToRatioDuration(WeaponLevel));
        damager.SetTargetSize(LevelToRatioAttackRange(WeaponLevel));
    }

    
    
    private void OnDestroy()
    {
        if (HasLeveledUp != null)
        {
            HasLeveledUp -= WeaponLevelUp;
            HasLeveledUp -= SetSpinWeapon;
            HasLeveledUp -= SetDamager;
        }
    }
    
}
