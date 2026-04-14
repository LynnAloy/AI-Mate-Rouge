using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenReferencePanel : Singleton<OpenReferencePanel>
{
    [SerializeField] private GameObject referencePanel;
    [SerializeField] private Button referenceButton;

    private bool hasOpenPanel = false;

    // Start is called before the first frame update
    void Start()
    {
        referenceButton.interactable = false;
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

    public void CanInteractWithButton(bool canInteract)
    {
        referenceButton.interactable = canInteract;
    }
}
