using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalentUIController : Singleton<TalentUIController>
{
    [SerializeField] private GameObject talentButton;
    [SerializeField] private Transform content;
    [SerializeField] private TMP_Text ownedTC;

    private List<TalentSO> talents = new();
    private List<TalentButtonUIController> buttons = new();

    private bool hasInitedButton = false;

    protected override void Awake()
    {
        base.Awake();
    }


    private void Start()
    {

        talents = TalentController.Instance.GetTalentList();
        ownedTC.text = "天赋点:  " + TalentCurrencyController.Instance.GetTalentCurrencyOwned();
        SetUpTalentButton();
    }


    public void SetUpTalentButton()
    {
        //Debug.Log("TalentUIController: Exe SetUpTalentButton");
        if(!hasInitedButton)
        {
            talents = TalentController.Instance.GetTalentList();
            foreach(var talent in talents)
            {
                var button = Instantiate(talentButton,  content);
                TalentButtonUIController tbUIC = button.GetComponent<TalentButtonUIController>();
                buttons.Add(tbUIC);
                button.GetComponent<TalentButtonUIController>().SetUpTalentButtonUI(talent);
                tbUIC.OnOwnedTCChanged += SetOwnedTC;
            }
        }
        hasInitedButton = true;
    }

    private void SetOwnedTC()
    {
        ownedTC.text = "天赋点:  " + TalentCurrencyController.Instance.GetTalentCurrencyOwned();
    }

    private void OnDestroy()
    {
        foreach(var button in buttons)
        {
            if(button != null)
            {
                TalentButtonUIController tbUIC = button.GetComponent<TalentButtonUIController>();
                tbUIC.OnOwnedTCChanged -= SetOwnedTC; 
            }
        }
    }
}
