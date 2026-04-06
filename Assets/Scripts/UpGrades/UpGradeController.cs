using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGradeController : Singleton<UpGradeController>
{
    [SerializeField] private List<UpGradeSO> upGradeList;

    public Action OnTempUpGradeListReady;

    private List<UpGradeSO> tempUpGradeList = new();
    private List<Weapon> weapons = new();
    private PlayerController player;
    private PlayerHealthController playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        weapons = PlayerController.Instance.GetWeapons();
        player = PlayerController.Instance;
        playerHealth = PlayerHealthController.Instance;
        WaveController.Instance.OnWaveEnd += PrepareUpGrades;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PrepareUpGrades()
    {
        tempUpGradeList.Clear();
        ShuffleList(upGradeList);
        for (int i = 0; i < 4; i++)//hashcode here, maybe changed by difficulty system
        {
            tempUpGradeList.Add(upGradeList[i]);
        }
        OnTempUpGradeListReady?.Invoke();
    }

    public void ApplyUpGrade(UpGradeSO upGradeSO)
    {
        player.SetPickUpRange(upGradeSO.PlayerPickUpRangeUpRatio);
        Debug.Log($"UpGradeController: CurrentPickUpRange - {player.GetPickUpRange()}");
        player.SetPlayerMoveSpeed(upGradeSO.PlayerMoveSpeedUpRatio);
        Debug.Log($"UpGradeController: CurrentMoveSpeed - {player.GetPlayerMoveSpeed()}");
        playerHealth.SetMaxHealth(upGradeSO.PlayerHealthUpRatio);
        Debug.Log($"UpGradeController: CurrentHealth - {playerHealth.GetCurrentHealth()}");
        foreach (var weapon in weapons)
        {
            weapon.WeaponLevelUp(upGradeSO.WeaponLevelUpAmount);
            Debug.Log($"UpGradeController: CurrentWeaponLevel - {weapon.WeaponLevel}");
            weapon.SetWeaponDamage(upGradeSO.WeaponDamageUpRatio);
            Debug.Log($"UpGradeController: CurrentWeaponDamage - {weapon.GetWeaponDamage()}");
        }
    }

    private void ShuffleList(List<UpGradeSO> upGrades)
    {
        Debug.Log("UpGradeController: Shuffling UpGrade List");
        int n = upGrades.Count;
        for(int i = n - 1; i >= 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (upGrades[i], upGrades[j]) = (upGrades[j], upGrades[i]);
        }
    }

    public List<UpGradeSO> GetTempUpGradeList()
    {
        return tempUpGradeList;
    }

    private void OnDestroy()
    {
        if(WaveController.Instance != null)
        {
            WaveController.Instance.OnWaveEnd -= PrepareUpGrades;
        }
    }
}
