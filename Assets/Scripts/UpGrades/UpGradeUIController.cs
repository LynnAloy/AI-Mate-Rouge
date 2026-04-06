using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpGradeUIController : Singleton<UpGradeUIController>
{
    /// <summary>
    /// 用于生成UpGradeButton
    /// 
    /// </summary>
    [SerializeField] private GameObject upGradeUIPanel;
    [SerializeField] private GameObject upGradeButtonPanel;
    [SerializeField] private GameObject upGradeButton;

    private List<UpGradeSO> tempUpGradeList;
    private List<UpGradeButtonUIController> buttons;
    private bool hasInitedButtons = false;

    protected override void Awake()
    {
        base.Awake();
        upGradeUIPanel.SetActive(false);
        tempUpGradeList = new();
        buttons = new();
        
    }

    private void OnEnable()
    {
        if(UpGradeController.Instance != null)
        {
            UpGradeController.Instance.OnTempUpGradeListReady += SetUpUpGradeButton;
            UpGradeController.Instance.OnTempUpGradeListReady += UpdateUpGradeButton;
        }
    }

    private void OnDisable()
    {
        if (UpGradeController.Instance != null)
        {
            UpGradeController.Instance.OnTempUpGradeListReady -= SetUpUpGradeButton;
            UpGradeController.Instance.OnTempUpGradeListReady -= UpdateUpGradeButton;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetUpUpGradeButton()
    {
        if (!hasInitedButtons)
        {
            Debug.Log("UpGrafeUIController: Start setting up upgrade button.");
            tempUpGradeList = UpGradeController.Instance.GetTempUpGradeList();
            for (int i = 0; i < 4; i++)
            {
                var button = Instantiate(upGradeButton, upGradeButtonPanel.transform);
                UpGradeButtonUIController buttonController = button.GetComponent<UpGradeButtonUIController>();
                buttons.Add(buttonController);
                button.GetComponent<UpGradeButtonUIController>().SetUpGardeButtonUI(tempUpGradeList[i]);

            }
        }
        hasInitedButtons = true;
    }

    private void UpdateUpGradeButton()
    {
        if (hasInitedButtons)
        {
            tempUpGradeList = UpGradeController.Instance.GetTempUpGradeList();
            for (int i = 0; i < 4; i++)
            {
                buttons[i].GetComponent<UpGradeButtonUIController>().SetUpGardeButtonUI(tempUpGradeList[i]);

            }
        }
    }

    public GameObject GetUpGradeUIPanel()
    {
        return upGradeUIPanel;
    }

    private void OnDestroy()
    {
        if(UpGradeController.Instance != null)
        {
            UpGradeController.Instance.OnTempUpGradeListReady -= SetUpUpGradeButton;
            UpGradeController.Instance.OnTempUpGradeListReady -= UpdateUpGradeButton;
        }
    }
}
