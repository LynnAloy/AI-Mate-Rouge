using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDifficultyPanel : MonoBehaviour
{
    [SerializeField] private GameObject difficultyPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClosePanel()
    {
        difficultyPanel.SetActive(false);
    }
}
