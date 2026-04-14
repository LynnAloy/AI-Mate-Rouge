using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : Singleton<CoinController>
{
    [SerializeField] private int currentCoinAmount = 0;
    [SerializeField] private CoinPickUp coinPickUp;

    public Action OnCoinOwnedChange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCoin(int coinValue)
    {
        currentCoinAmount += coinValue;
        OnCoinOwnedChange?.Invoke();
    }

    public void SpawnCoin(Vector3 position, int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            Instantiate(coinPickUp, position, Quaternion.identity);
        }
    }

    public int GetCurrentCoinAmount()
    {
        return currentCoinAmount;
    }
}
