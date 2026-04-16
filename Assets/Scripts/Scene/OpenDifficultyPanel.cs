using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDifficultyPanel : MonoBehaviour
{
    [SerializeField] private GameObject difficultyPanel;

    private void Awake()
    {
        difficultyPanel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPanel()
    {
        difficultyPanel.SetActive(true);
    }
}
