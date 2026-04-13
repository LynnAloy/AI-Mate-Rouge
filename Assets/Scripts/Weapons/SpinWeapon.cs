using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpinWeapon : Weapon
{
    [SerializeField] private float spawnInterval;
    [SerializeField] private Transform holder, fireballToSpawn;
    //local variable
    private float spawnTimer;
    private float baseAttackRange;
    private float baseAttackSpeed;

    protected override void Awake()
    {
        base.Awake();
        spawnTimer = spawnInterval;
        baseAttackRange = attackRange;
        baseAttackSpeed = attackSpeed;
    }

    private void Start()
    {
        InitWeapon();
        HasLeveledUp += SetSpinWeapon;
    }

    private void Update()
    {
        holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + attackSpeed * Time.deltaTime * LevelToRatioAttackSpeed(WeaponLevel));
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0f)
        {
            spawnTimer = spawnInterval;
            var instanceObject = Instantiate(fireballToSpawn, fireballToSpawn.position, fireballToSpawn.rotation, holder);
            Debug.Log($"SpinWeapon: Spawned fireball is {instanceObject == null}");
            instanceObject.gameObject.SetActive(true);
        }
    }

    private void InitWeapon()
    {
        transform.localScale = Vector3.one * attackRange;
        Debug.Log($"SpinWeapon: The attack range of SpinWeapon is {transform.localScale}");
    }

    private void SetSpinWeapon()
    {
        attackRange = baseAttackRange * LevelToRatioAttackRange(WeaponLevel);
        Debug.Log($"SpinWeapon: AttackRange updated to {attackRange}.");
        attackSpeed = baseAttackSpeed * LevelToRatioAttackSpeed(WeaponLevel);
        Debug.Log($"SpinWeapon: AttackSpeed updated to {attackSpeed}.");
    }

    private void OnDestroy()
    {
        if (HasLeveledUp != null)
        {
            HasLeveledUp -= SetSpinWeapon;
        }
    }
    
}
