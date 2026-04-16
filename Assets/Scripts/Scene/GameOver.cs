using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject parentPanel;
    [SerializeField] private GameObject endGamePanel_Lose;
    [SerializeField] private GameObject endGamePanel_Win;
    [SerializeField] private TMP_Text rewardText;

    private void Awake()
    {
        endGamePanel_Lose.SetActive(false);
        endGamePanel_Win.SetActive(false);
        parentPanel.SetActive(false);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealthController.Instance.OnPlayerDie += OnGameOverLose;
        WaveController.Instance.OnGameWin += OnGmaeOverWin;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGameOverLose()
    {
        Time.timeScale = 0;
        endGamePanel_Lose.SetActive(true);
        parentPanel.SetActive(true);
        int talentCurrencyReward = WaveController.Instance.GetCurrentWave();
        TalentCurrencyController.Instance.AddTalentCurrency(talentCurrencyReward);
        rewardText.text = "获得天赋点:  " + talentCurrencyReward; 
    }

    private void OnGmaeOverWin()
    {
        Time.timeScale = 0;
        endGamePanel_Win.SetActive(true);
        parentPanel.SetActive(true);
        int talentCurrencyReward = WaveController.Instance.GetCurrentWave();
        TalentCurrencyController.Instance.AddTalentCurrency(talentCurrencyReward);
        rewardText.text = "获得天赋点:  " + talentCurrencyReward;
    }

    private void OnDestroy()
    {
        if(PlayerHealthController.Instance != null)
        {
            PlayerHealthController.Instance.OnPlayerDie -= OnGameOverLose;
        }
        if(WaveController.Instance != null)
        {
            WaveController.Instance.OnGameWin -= OnGmaeOverWin;
        }
    }

}
