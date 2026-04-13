using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayOwnedKeyWord : MonoBehaviour
{
    [SerializeField] private TMP_Text ownedKeyWordsText;

    private Dictionary<string, int> keyWordsOwned = new();

    private void Awake()
    {
        ownedKeyWordsText.text = "当前拥有的嵌入词: \n";
        OpenAIPanel.Instance.OnFreshKeyWordsDisplay += UpdateDisplay;
    }

    // Start is called before the first frame update
    void Start()
    {
        keyWordsOwned = KeyWordController.Instance.GetKeyWordsOwned();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateDisplay()
    {
        keyWordsOwned = KeyWordController.Instance.GetKeyWordsOwned();
        ownedKeyWordsText.text = "当前拥有的嵌入词: \n";
        foreach (var keyWord in keyWordsOwned)
        {
            ownedKeyWordsText.text += $"嵌入词: {keyWord.Key} 数量: {keyWord.Value}     ";
        }
    }

    private void OnDestroy()
    {
        if(OpenAIPanel.Instance != null)
        {
            OpenAIPanel.Instance.OnFreshKeyWordsDisplay -= UpdateDisplay;
        }
    }
}
