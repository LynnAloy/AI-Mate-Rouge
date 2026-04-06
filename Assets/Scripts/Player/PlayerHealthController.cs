using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : Singleton<PlayerHealthController>
{
    [SerializeField] private float maxHealth;
    [SerializeField] private Slider healthbar;
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10f);
        }
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
