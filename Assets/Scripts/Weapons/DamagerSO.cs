using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Damager")]
public class DamagerSO : ScriptableObject
{
    [SerializeField] private float damageAmount;//the damage of damager
    [SerializeField] private float damagerSize;//damager的实际作用大小
    [SerializeField] private bool isExternal;//whether the intance of damager will be destroyed
    [SerializeField] private bool destroyParent;//whether also destroy the parent
    [SerializeField] private bool canChangedSize;//whether the damager will change size with time
    [SerializeField] private bool canKnockBack;//whether can kock back enemy
    [SerializeField] private float knockBackDistance;//how far can knock enemy
    [SerializeField] private float lifeTime;//the existing time of not external damager
    [SerializeField] private float changeSizeTime;//the time a cycle of damager changed size
    [SerializeField] private bool isAreaDamage;//whether the damager cause area damage
    [SerializeField] private float areaDamageInterval;//how often cause damage for area attack weapon
    [SerializeField] private bool destroyOnImpact;//whether the damager will be destroyed when hit enemy

    //gets and sets
    public float DamageAmount
    {
        get => damageAmount;
        set => damageAmount = value;
    }

    public float DamagerSize
    {
        get => damagerSize;
        set => damagerSize = value;
    }

    public bool IsExternal => isExternal;
    public bool DestroyParent => destroyParent;
    public bool CanChangedSize => canChangedSize;
    public bool CanKnockBack => canKnockBack;
    public float KnockBackDistance => knockBackDistance;
    public float LifeTime => lifeTime;
    public float ChangeSizeTime => changeSizeTime;
    public bool IsAreaDamage => isAreaDamage;
    public float AreaDamageInterval => areaDamageInterval;
    public bool DestroyOnImpact => destroyOnImpact;
}
