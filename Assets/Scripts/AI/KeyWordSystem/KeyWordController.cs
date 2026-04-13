using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyWordController : Singleton<KeyWordController>
{
    [SerializeField] private KeyWordSO keyWordSO;
    [SerializeField] private KeyWordPickUp keyWordPickUp;

    private List<string> keyWords = new();
    private Dictionary<string, int> keyWordsOwned = new();


    protected override void Awake()
    {
        base.Awake();
        keyWords = keyWordSO.KeyWords;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnKeyWord(Vector3 position)
    {
        Instantiate(keyWordPickUp, position, Quaternion.identity);
    }

    public void AddKeyWord()
    {
        int randomIndex = UnityEngine.Random.Range(0, keyWords.Count);
        string keyWordToAdd = keyWords[randomIndex];
        if (keyWordsOwned.ContainsKey(keyWordToAdd))
        {
            keyWordsOwned[keyWordToAdd]++;
        }
        else
        {
            keyWordsOwned.Add(keyWordToAdd, 1);
        }
    }

    public List<string> GetKeyWords()
    {
        return keyWords;
    }

    public Dictionary<string, int> GetKeyWordsOwned()
    {
        return keyWordsOwned;
    }
}
