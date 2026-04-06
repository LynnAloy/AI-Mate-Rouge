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
    [SerializeField] private float damage;
    [SerializeField] private bool isExternal;
    [SerializeField] private bool canChangeSize;
    [SerializeField] private bool canKnockBack;
    [SerializeField] private float knockBackDistance;
    [SerializeField] private bool destoryParent;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private float timeBetweenAttack;
    [SerializeField] private int amount;
    [SerializeField] private float duration;



    public Sprite WeaponSprite => weaponSprite;
    public float Damage => damage;
    public bool IsExternal => isExternal;
    public bool CanChangeSize => canChangeSize;
    public bool CanKnockBack => canKnockBack;
    public float KnockBackDistance => knockBackDistance;
    public bool DestoryParent => destoryParent;
    public float AttackSpeed => attackSpeed;
    public float AttackRange => attackRange;
    public float TimeBetweenAttack => timeBetweenAttack;
    public int Amount => amount;
    public float Duration => duration;
}
