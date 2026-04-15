using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpGradeButtonUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text description;
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text title;
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text cost;


    private List<UpGradeSO> upGrades = new();
    private UpGradeSO currentUpGrade;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpGardeButtonUI(UpGradeSO upGradeSO)
    {
        //Debug.Log("UpGradeButtonUIController: Setting up a button details.");
        currentUpGrade = upGradeSO;
        title.text = upGradeSO.UpGradeName;
        description.text = upGradeSO.UpGradeDescription;
        icon.sprite = upGradeSO.UpGradeIcon;
        cost.text = upGradeSO.Cost.ToString();
        if (button != null)
        {
            button.onClick.RemoveListener(OnButtonClicked);
            button.onClick.AddListener(OnButtonClicked);
            button.interactable = true;
            if(CoinController.Instance.GetCurrentCoinAmount() < upGradeSO.Cost)
            {
                button.interactable = false;
            }
        }

    }

    private void OnButtonClicked()
    {
        UpGradeController.Instance.ApplyUpGrade(currentUpGrade);
        CoinController.Instance.SpendCoin(currentUpGrade.Cost);
        button.interactable = false;
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnButtonClicked);
    }
}
