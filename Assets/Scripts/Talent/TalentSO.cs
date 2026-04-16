using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Talent")]
public class TalentSO : ScriptableObject
{
    [SerializeField] private string talentName;
    [SerializeField] private string talentDescription;
    [SerializeField] private int talentCost;
    [SerializeField] private float playerMaxHealthUpRatio;
    [SerializeField] private float playerMoveSpeedUpRatio;
    [SerializeField] private bool isSEE;
    [SerializeField] private int canPurchaseTime;
    [SerializeField] private int hasPurchasedTime;

    public string TalentName => talentName;
    public string TalentDescription => talentDescription;
    public int TalentCost => talentCost;
    public float PlayerMaxHealthUpRatio => playerMaxHealthUpRatio;
    public float PlayerMoveSpeedUpRatio => playerMoveSpeedUpRatio;
    public bool IsSEE => isSEE;

    public int CanPurchaseTime
    {
        get => canPurchaseTime;
        set => canPurchaseTime = value;
    }

    public int HasPurchasedTime
    {
        get => hasPurchasedTime;
        set => hasPurchasedTime = value;
    }
}
