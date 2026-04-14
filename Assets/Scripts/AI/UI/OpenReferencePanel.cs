using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenReferencePanel : MonoBehaviour
{
    [SerializeField] private GameObject referencePanel;

    private bool hasOpenPanel = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hasOpenPanel)
        {
            if(Input.anyKeyDown)
            {
                referencePanel.SetActive(false);
                hasOpenPanel = false;
            }
        }
    }

    public void OpenRefPanel()
    {
        referencePanel.SetActive(true);
        hasOpenPanel = true;
    }
}
