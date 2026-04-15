using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentCurrencyController : Singleton<TalentCurrencyController>
{
    [SerializeField] private TalentCurrencySO talentCurrencySO;

    private int talentCurrencyOwned;

    protected override void Awake()
    {
        base.Awake();
        talentCurrencyOwned = talentCurrencySO.TalentCurrencyOwned;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTalentCurrency(int amount)
    {
        talentCurrencyOwned += amount;
        talentCurrencySO.TalentCurrencyOwned = talentCurrencyOwned;
    }
}
