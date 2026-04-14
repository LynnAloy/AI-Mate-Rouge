using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUIController : Singleton<CoinUIController>
{
    [SerializeField] private TMP_Text coinText;

    private int currentCoinAmount;

    protected override void Awake()
    {
        currentCoinAmount = CoinController.Instance.GetCurrentCoinAmount();
        CoinController.Instance.OnCoinOwnedChange += UpdateCoinUI;
        coinText.text = "金钱: " + currentCoinAmount;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateCoinUI()
    {
        currentCoinAmount = CoinController.Instance.GetCurrentCoinAmount();
        coinText.text = "金钱: " + currentCoinAmount;
    }

    private void OnDestroy()
    {
        if (CoinController.Instance != null)
        {
            CoinController.Instance.OnCoinOwnedChange -= UpdateCoinUI;
        }
    }
}

