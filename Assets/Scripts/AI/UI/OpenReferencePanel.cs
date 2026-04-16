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
        if (PlayerController.Instance.GetIsSEE())
        {
            Debug.Log($"OpenReferencePanel: IsSEE: {PlayerController.Instance.GetIsSEE()}");
            referenceButton.interactable = true;
        }
        else
        {
            referenceButton.interactable = false;
        }
        if(PlayerController.Instance != null)
        {
            PlayerController.Instance.OnSEEChanged += SetCanInteractWithButton;
        }
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

    private void SetCanInteractWithButton()
    {
        referenceButton.interactable = true;
    }

    private void OnDestroy()
    {
        if(PlayerController.Instance != null)
        {
            PlayerController.Instance.OnSEEChanged -= SetCanInteractWithButton;
        }
    }
}
