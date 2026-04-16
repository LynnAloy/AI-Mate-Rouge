using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : Singleton<PlayerHealthController>
{
    [SerializeField] private PlayerHealthControllerSO playerHealthControllerSO;
    [SerializeField] private Slider healthbar;

    private float maxHealth;
    private float currentHealth;

    public Action OnPlayerDie;

    protected override void Awake()
    {
        base.Awake();
        maxHealth = playerHealthControllerSO.MaxHealth;
        currentHealth = maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetHealthBar();
    }

    private void SetHealthBar()
    {
        healthbar.value = currentHealth / maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            OnPlayerDie?.Invoke();
        }
    }

    public void RecoverPlayerHealth(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void SetMaxHealth(float ratio)
    {
        maxHealth *= ratio;
        currentHealth = maxHealth;
    }

    public void SetMaxHealthExternal(float ratio)
    {
        Debug.Log($"ratio: {ratio}  maxHealth: {maxHealth}");
        maxHealth *= ratio;
        playerHealthControllerSO.MaxHealth = maxHealth;
        Debug.Log("PlayerHealthController: Invoked.");
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
