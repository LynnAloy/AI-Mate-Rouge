using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : Singleton<PlayerHealthController>
{
    [SerializeField] private float maxHealth;
    [SerializeField] private Slider healthbar;
    private float currentHealth;

    public Action OnPlayerDie;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
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

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
