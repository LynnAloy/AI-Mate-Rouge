using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaWeapon : Weapon
{
    [SerializeField] private EnemyDamager damager;
    private float baseAttackRange;
    private float baseAttackSpeed;
    private float baseTimeBetweenAttack;

    protected override void Awake()
    {
        base.Awake();
        baseAttackRange = attackRange;
        baseAttackSpeed = attackSpeed;
        baseTimeBetweenAttack = timeBetweenAttack;
    }


    // Start is called before the first frame update
    void Start()
    {
        HasLeveledUp += SetAreaWeapon;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetAreaWeapon()
    {
        attackRange = LevelToRatioAttackRange(WeaponLevel) * baseAttackRange;
        attackSpeed = LevelToRatioAttackSpeed(WeaponLevel) * baseAttackSpeed;
        timeBetweenAttack = LevelToTimeBetweenAttack(WeaponLevel) * baseTimeBetweenAttack;
    }

    private void OnDestroy()
    {
        if(HasLeveledUp != null)
        {
            HasLeveledUp -= SetAreaWeapon;
        }
    }
}
