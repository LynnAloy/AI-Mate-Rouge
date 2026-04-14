using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGradeController : Singleton<UpGradeController>
{
    [SerializeField] private List<UpGradeSO> upGradeList;

    public Action OnTempUpGradeListReady;
    public Action OnDamageChanged;

    private List<UpGradeSO> tempUpGradeList = new();
    private List<Weapon> assignedWeapons = new();
    private List<Weapon> runtimeUnassignedWeapons = new();
    private PlayerController player;
    private PlayerHealthController playerHealth;
    private EnemyDamager enemyDamager;

    // Start is called before the first frame update
    void Start()
    {
        assignedWeapons = PlayerController.Instance.GetAssignedWeapons();
        runtimeUnassignedWeapons = PlayerController.Instance.GetUnassignedWeapons();
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
        foreach(var weapon in assignedWeapons)
        {
            EnemyDamager enemyDamager = weapon.GetComponentInChildren<EnemyDamager>(true);
            Debug.Log($"UpGradeController: Damager's damage before {enemyDamager.GetDamageAmount()}");
        }
        tempUpGradeList.Clear();
        ShuffleList(upGradeList);
        for (int i = 0; i < 4; i++)//hashcode here, maybe changed by difficulty system
        {
            if (upGradeList[i].GetNewWeapon == true)
            {
                if (player != null && runtimeUnassignedWeapons.Count != 0)
                {
                    tempUpGradeList.Add(upGradeList[i]);
                }
                else
                {
                    for(int j = i + 1; j < upGradeList.Count; j++)
                    {
                        if (upGradeList[j].GetNewWeapon == false)
                        {
                            tempUpGradeList.Add(upGradeList[j]);
                        }
                    }
                }
            }
            else
            {
                tempUpGradeList.Add(upGradeList[i]);
            }
        }
        OnTempUpGradeListReady?.Invoke();
    }

    public void ApplyUpGrade(UpGradeSO upGradeSO)
    {
        if (upGradeSO.PlayerPickUpRangeUpRatio != 1)
        {
            player.SetPickUpRange(upGradeSO.PlayerPickUpRangeUpRatio);
            Debug.Log($"UpGradeController: CurrentPickUpRange - {player.GetPickUpRange()}");
        }
        if (upGradeSO.PlayerMoveSpeedUpRatio != 1)
        {
            player.SetPlayerMoveSpeed(upGradeSO.PlayerMoveSpeedUpRatio);
            Debug.Log($"UpGradeController: CurrentMoveSpeed - {player.GetPlayerMoveSpeed()}");
        }
        if (upGradeSO.PlayerHealthUpRatio != 1)
        {
            playerHealth.SetMaxHealth(upGradeSO.PlayerHealthUpRatio);
            Debug.Log($"UpGradeController: CurrentHealth - {playerHealth.GetCurrentHealth()}");
        }
        foreach (var weapon in assignedWeapons)
        {
            Debug.Log($"UpGradeController: Weapon is {weapon == null}");
            enemyDamager = weapon.GetComponentInChildren<EnemyDamager>(true);
            Debug.Log($"UpGradeController: EnemyDamager in weapon is {enemyDamager == null}");
            if (upGradeSO.WeaponLevelUpAmount != 0)
            {
                weapon.WeaponLevelUp(upGradeSO.WeaponLevelUpAmount);
            }
            Debug.Log($"UpGradeController: CurrentWeaponLevel - {weapon.WeaponLevel}");
            Debug.Log($"UpGradeController: Current Damager damage: {enemyDamager.GetDamageAmount()}");
            if (upGradeSO.WeaponDamageUpRatio != 1)
            {
                enemyDamager.SetDamageAmount(enemyDamager.GetDamageAmount() * upGradeSO.WeaponDamageUpRatio);
            }
            Debug.Log($"UpGradeController: CurrentWeaponDamage - {enemyDamager.GetDamageAmount()}");
        }
        OnDamageChanged?.Invoke();
        if (upGradeSO.GetNewWeapon)
        {
            player.AddWeapon(player.GetRandomWeaponIndex());
        }
        if(upGradeSO.CanUnlockKeyWordManul)
        {
            OpenReferencePanel.Instance.CanInteractWithButton(true);
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
