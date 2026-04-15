using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutSceneController : MonoBehaviour
{
    [SerializeField] private GameObject TutPanel;

    private void Awake()
    {
        Time.timeScale = 0.0f;
        TutPanel.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1.0f;
            TutPanel.SetActive(false);
        }
    }
}
