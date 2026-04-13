using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UpGrade")]
public class UpGradeSO : ScriptableObject
{
    [SerializeField] private string upGradeName;
    [SerializeField] private string upGradeDescription;
    [SerializeField] private Sprite upGradeIcon;
    [SerializeField] private int weaponLevelUpAmount;
    [SerializeField] private int playerLevelUpAmount;
    [SerializeField] private float playerHealthUpRatio;
    [SerializeField] private float weaponDamageUpRatio;
    [SerializeField] private float playerMoveSpeedUpRatio;
    [SerializeField] private float playerPickUpRangeUpRatio;
    [SerializeField] private bool getNewWeapon;

    public string UpGradeName => upGradeName;
    public string UpGradeDescription => upGradeDescription;
    public Sprite UpGradeIcon => upGradeIcon;
    public int WeaponLevelUpAmount => weaponLevelUpAmount;
    public int PlayerLevelUpAmount => playerLevelUpAmount;
    public float PlayerHealthUpRatio => playerHealthUpRatio;
    public float WeaponDamageUpRatio => weaponDamageUpRatio;
    public float PlayerMoveSpeedUpRatio => playerMoveSpeedUpRatio;
    public float PlayerPickUpRangeUpRatio => playerPickUpRangeUpRatio;
    public bool GetNewWeapon => getNewWeapon;

}
