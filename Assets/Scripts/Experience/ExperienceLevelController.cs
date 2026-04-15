using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceLevelController : Singleton<ExperienceLevelController>
{
    //Variables
    [SerializeField] private int currentExperience;
    //a, b stands for the formula y = a * x ^ b y is the experience needed for the next level
    //x stands for the current level
    [SerializeField] private float a;//2 current for the best
    [SerializeField] private float b;//1.8 current for the best
    [SerializeField] private int currentLevel;
    //References
    [SerializeField] private ExpPickup expPickup;

    //For testing leveling weapon
    //public Action HasLeveledUp;

    private int experienceToNextLevel;
    // Start is called before the first frame update
    void Start()
    {
        experienceToNextLevel = Mathf.RoundToInt(a * Mathf.Pow(currentLevel, b));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetExp(int expToGet)
    {
        currentExperience += expToGet;
        if(currentExperience >= experienceToNextLevel)
        {
            LevelUp();
        }
        ExperienceUIController.Instance.UpdateExperienceSlider(currentExperience, experienceToNextLevel);
        ExperienceUIController.Instance.UpdateExperienceText(currentLevel);
    }

    public void SpawnExp(Vector3 position, int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            Instantiate(expPickup, position, Quaternion.identity);
        }
    }

    private void LevelUp()
    {
        currentExperience -= experienceToNextLevel;
        currentLevel++;
        experienceToNextLevel = Mathf.RoundToInt(a * Mathf.Pow(currentLevel, b));
        //For testing leveling weapon
        Debug.Log("Level up.");
        //HasLeveledUp?.Invoke();
    }

    public void LevelUpGrade(int upAmount)
    {
        currentLevel += upAmount;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
