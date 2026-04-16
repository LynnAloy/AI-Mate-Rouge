using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player")]
public class PlayerSO : ScriptableObject
{
    [SerializeField] private float playerMoveSpeed;
    [SerializeField] private bool isSEE;//short for is software engineering engineer

    public float PlayerMoveSpeed
    {
        get => playerMoveSpeed;
        set => playerMoveSpeed = value;
    }

    public bool IsSEE
    {
        get => isSEE;
        set => isSEE = value;
    }

}
