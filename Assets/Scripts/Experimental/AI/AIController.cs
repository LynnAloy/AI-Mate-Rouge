using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class AIController : Singleton<AIController>
{
    //Variables
    [SerializeField] private int currentLevel;
    [SerializeField] private float healInterval;
    [SerializeField] private float healRange;
    [SerializeField] private float healAmount;
    [SerializeField] private List<Weapon> unassignedWeapon;
    [SerializeField] private List<Weapon> assignedWeapon;
    [SerializeField] private TMP_Text warningText;
    [SerializeField] private float a;//for log base
    //Reference
    [SerializeField] private CircleCollider2D healRangeCollider;
    private bool canHeal = false;
    private float healCounter;//count down for healInterval
    private List<Weapon> runtimeUnassignedWeapon = new();

    private bool canAIAttackEnemy = false;
    private bool canAIAttackPlayer = false;

    private bool canPickUpExp = false;
    private bool canPickUpKeyWord = false;
    private bool canPickUpCoin = false;

    private CancellationTokenSource cts;

    private HashSet<EnemyController> enemiesInHealRange = new();
    private HashSet<PlayerController> playerInHealRange = new();

    protected override void Awake()
    {
        base.Awake();
        runtimeUnassignedWeapon = unassignedWeapon;
        healCounter = healInterval;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canHeal)
        {
            healCounter -= Time.deltaTime;
            {
                if (healCounter <= 0)
                {
                    healCounter = healInterval;
                    if (enemiesInHealRange.Count > 0)
                    {
                        var enemies = new EnemyController[enemiesInHealRange.Count];
                        enemiesInHealRange.CopyTo(enemies);
                        foreach (var enemy in enemies)
                        {
                            if (enemy != null)
                            {
                                enemy.RecoverEnemyHealth(healAmount);
                            }
                            else
                            {
                                enemiesInHealRange.Remove(enemy);
                            }
                        }
                    }
                    else if (playerInHealRange.Count > 0)
                    {
                        var players = new PlayerController[playerInHealRange.Count];
                        playerInHealRange.CopyTo(players);
                        foreach (var player in players)
                        {
                            if (player != null)
                            {
                                var playerHealth = player.GetComponent<PlayerHealthController>();
                                if (playerHealth != null)
                                {
                                    playerHealth.RecoverPlayerHealth(healAmount);
                                }
                            }
                            else
                            {
                                playerInHealRange.Remove(player);
                            }
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent<EnemyController>(out var enemy))
            {
                enemiesInHealRange.Add(enemy);
            }
        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent<PlayerController>(out var player))
            {
                playerInHealRange.Add(player);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent<EnemyController>(out var enemy))
            {
                enemiesInHealRange.Remove(enemy);
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent<PlayerController>(out var player))
            {
                playerInHealRange.Remove(player);
            }
        }
    }

    public void GiveWeapon()
    {
        if(runtimeUnassignedWeapon.Count == 0)
        {
            ShowWarning(warningText, "没有足够纳米材料打印武器");
            return;
        }
        int randomIndex = UnityEngine.Random.Range(0, runtimeUnassignedWeapon.Count);
        Weapon weapon = runtimeUnassignedWeapon[randomIndex];
        weapon.gameObject.SetActive(true);
        assignedWeapon.Add(weapon);
        runtimeUnassignedWeapon.RemoveAt(randomIndex);
        MarkAIWeapon(weapon);
        if(canAIAttackPlayer)
        {
            MarkCanDamagePlayer(weapon);
        }
    }

    private void MarkAIWeapon(Weapon weapon)
    {
        var damagers = weapon.GetComponentsInChildren<EnemyDamager>();
        foreach (var damager in damagers)
        {
            if (damager != null)
            {
                damager.SetIsAIWeapon(true);
            }
        }
    }

    private void MarkCanDamagePlayer(Weapon weapon)
    {
        var damagers = weapon.GetComponentsInChildren<EnemyDamager>();
        foreach (var damager in damagers)
        {
            if (damager != null)
            {
                damager.SetCanDamagePlayer(canAIAttackPlayer);
            }
        }
    }

    public void SetCurrentLevel(int currentLevel)
    {
        if (currentLevel <= ExperienceLevelController.Instance.GetCurrentLevel())
        {
            this.currentLevel = currentLevel;
        }
        else
        {
            ShowWarning(warningText, "不允许下克上！想赋予的等级太高");
            return;
        }
        SetHealRange(currentLevel);
    }

    private void SetHealRange(int level)
    {
        healRangeCollider.radius = Mathf.Log(level, a) + 1.3f;
    }

    public void SetHeal(bool canHeal)
    {
        this.canHeal = canHeal;
    }

    public bool GetHeal()
    {
        return canHeal;
    }

    public void SetHealAmount(float healAmount)
    {
        this.healAmount = healAmount;
    }

    public float GetHealAmount()
    {
        return healAmount;
    }

    public void SetCanAIAttackEnemy(bool canAIAttackEnemy)
    {
        this.canAIAttackEnemy = canAIAttackEnemy;
    }

    public bool GetCanAIAttackEnemy()
    {
        return canAIAttackEnemy;
    }

    public void SetCanAIAttackPlayer(bool canAIAttackPlayer)
    {
        this.canAIAttackPlayer = canAIAttackPlayer;
    }

    public bool GetCanAIAttackPlayer()
    {
        return canAIAttackPlayer;
    }

    public void SetCanPickUpExp(bool canPickUpExp)
    {
        this.canPickUpExp = canPickUpExp;
    }

    public bool GetCanPickUpExp()
    {
        return canPickUpExp;
    }

    public void SetCanPickUpKeyWord(bool canPickUpKeyWord)
    {
        this.canPickUpKeyWord = canPickUpKeyWord;
    }

    public bool GetCanPickUpKeyWord()
    {
        return canPickUpKeyWord;
    }

    public void SetCanPickUpCoin(bool canPickUpCoin)
    {
        this.canPickUpCoin = canPickUpCoin;
    }

    public bool GetCanPickUpCoin()
    {
        return canPickUpCoin;
    }

    private async void ShowWarning(TMP_Text warningText, string message)
    {
        cts?.Cancel();
        cts = new CancellationTokenSource();
        await ShowWarningAsync(warningText, message, cts.Token);
    }

    private async Task ShowWarningAsync(TMP_Text warningText, string message, CancellationToken ct)
    {
        warningText.gameObject.SetActive(true);
        warningText.text = message;
        try
        {
            await Task.Delay(TimeSpan.FromSeconds(2), ct);
            warningText.gameObject.SetActive(false);
        }
        catch (TaskCanceledException)
        {
            // Ignore cancellation
        }
    }

    public void CancelWarning()
    {
        cts?.Cancel();
    }
}
