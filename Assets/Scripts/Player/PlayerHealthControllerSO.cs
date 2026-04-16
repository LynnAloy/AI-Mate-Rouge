using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PlayerHealth")]
public class PlayerHealthControllerSO : ScriptableObject
{
    [SerializeField] private float maxHealth;

    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }
}
