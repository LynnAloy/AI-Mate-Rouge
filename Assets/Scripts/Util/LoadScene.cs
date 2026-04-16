using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MyLoadScene()
    {
        if(sceneName == null)
        {
            Debug.LogError("LoadScene: SceneName is null");
        }
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneAndChangeTS()
    {
        if (sceneName == null)
        {
            Debug.LogError("LoadScene: SceneName is null");
        }
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneName);
    }
       
}
