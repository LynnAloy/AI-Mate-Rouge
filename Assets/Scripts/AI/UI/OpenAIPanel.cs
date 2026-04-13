using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OpenAIPanel : Singleton<OpenAIPanel>
{
    [SerializeField] private GameObject aiPanel;
    [SerializeField] private GameObject upGradePanel;
    [SerializeField] private GameObject errorText;

    public Action OnFreshKeyWordsDisplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void OpenAIPanelAndCloseUpGradePanel()
    {
        aiPanel.SetActive(true);
        OnFreshKeyWordsDisplay?.Invoke();
        errorText.SetActive(false);
        upGradePanel.SetActive(false);
    }
}
