using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TalentCurrency")]
public class TalentCurrencySO : ScriptableObject
{
    [SerializeField] private int talentCurrencyOwned;

    public int TalentCurrencyOwned
    {
        get => talentCurrencyOwned;
        set => talentCurrencyOwned = value;
    }
}
