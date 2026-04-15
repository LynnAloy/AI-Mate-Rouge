using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DescriptionPanelController : MonoBehaviour
{
    [SerializeField] private GameObject DescriptionPanel;
    [SerializeField] private string sceneName;

    private bool hasPanelOpen = false;

    private void Awake()
    {
        DescriptionPanel.SetActive(false);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        VideoController.Instance.OnVideoStop += OpenPanel;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasPanelOpen)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    private void OpenPanel()
    {
        DescriptionPanel.SetActive(true);
        hasPanelOpen = true;
    }

    private void OnDestroy()
    {
        if(VideoController.Instance != null)
        {
            VideoController.Instance.OnVideoStop -= OpenPanel;
        }
    }
}
