using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ExperienceUIController : Singleton<ExperienceUIController>
{
    //References
    [SerializeField] private Slider expSlider;
    [SerializeField] private TMP_Text expText;

    // Start is called before the first frame update
    void Start()
    {
        if (expSlider == null)
        {
            Debug.LogError("ExperienceUIController: expSlider is null.");
        }
        if (expText == null)
        {
            Debug.LogError("ExperienceUIController: expText is null.");
        }
        expSlider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateExperienceSlider(int currentExp, int expToNextLevel)
    {
        expSlider.value = (float)currentExp / (float)expToNextLevel;
    }

    public void UpdateExperienceText(int currentLevel)
    {
        expText.text = "Level: " + currentLevel;
    }
}
