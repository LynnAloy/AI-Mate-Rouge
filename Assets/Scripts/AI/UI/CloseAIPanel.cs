using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAIPanel : MonoBehaviour
{
    [SerializeField] private GameObject aiPanel;
    [SerializeField] private GameObject upGradePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseAIPanelAndOpenUpGradePanel()
    {
        upGradePanel.SetActive(true);
        aiPanel.SetActive(false);
    }
}
