using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType
{
    SpinWeapon,
    RangedWeapon,
    MeleeWeapon
}

[CreateAssetMenu(menuName = "Weapon")]

public class WeaponSO : ScriptableObject
{
    [SerializeField] private Sprite weaponSprite;
    [SerializeField] private float attackSpeed;//用于SpinWeapon，damager旋转的速度
    [SerializeField] private float attackRange;//用于SpinWeapon时表示damager实例距离玩家的range,projectile时为攻击距离
    [SerializeField] private float timeBetweenAttack;//用于projectile表示射击interval


    public Sprite WeaponSprite => weaponSprite;
    public float AttackSpeed => attackSpeed;
    public float AttackRange => attackRange;
    public float TimeBetweenAttack => timeBetweenAttack;
}
