using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextWave : MonoBehaviour
{
    [SerializeField] private GameObject upGradePanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClicked()
    {
        StartNextWave();
    }

    private void StartNextWave()
    {
        upGradePanel.SetActive(false);
        WaveController.Instance.ResumeGame();
    }
}
