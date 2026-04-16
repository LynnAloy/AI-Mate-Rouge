using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalentButtonUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text talentName;
    [SerializeField] private TMP_Text talentDescription;
    [SerializeField] private TMP_Text talentCost;
    [SerializeField] private Button button;
    [SerializeField] private Slider slider;

    private TalentSO currentTalentSO;

    public Action OnOwnedTCChanged;


    public void SetUpTalentButtonUI(TalentSO talentSO)
    {
        currentTalentSO = talentSO;
        talentName.text = talentSO.TalentName;
        talentDescription.text = talentSO.TalentDescription;
        talentCost.text = "天赋点  " + talentSO.TalentCost;
        slider.value = (float)talentSO.HasPurchasedTime / talentSO.CanPurchaseTime;
        if(button != null)
        {
            button.onClick.RemoveListener(OnButtonClicked);
            button.onClick.AddListener(OnButtonClicked);
            button.interactable = true;
            if(TalentCurrencyController.Instance.GetTalentCurrencyOwned() < talentSO.TalentCost)
            {
                button.interactable = false;
            }
            if(talentSO.CanPurchaseTime == talentSO.HasPurchasedTime)
            {
                button.interactable = false;
            }
        }
    }

    private void OnButtonClicked()
    {
        TalentController.Instance.RequestApplyTalent(currentTalentSO);
        TalentCurrencyController.Instance.SpendTalentCurrency(currentTalentSO.TalentCost);
        OnOwnedTCChanged?.Invoke();
        currentTalentSO.HasPurchasedTime++;
        UpdateButtonUI();
    }

    private void UpdateButtonUI()
    {
        slider.value = (float)currentTalentSO.HasPurchasedTime / currentTalentSO.CanPurchaseTime;
        if (TalentCurrencyController.Instance.GetTalentCurrencyOwned() < currentTalentSO.TalentCost)
        {
            button.interactable = false;
        }
        if (currentTalentSO.CanPurchaseTime == currentTalentSO.HasPurchasedTime)
        {
            button.interactable = false;
        }
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnButtonClicked);
    }
}
