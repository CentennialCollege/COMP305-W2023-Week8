using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Properties")] 
    public int value = 100;

    [Header("Display Properties")] 
    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponentInChildren<Slider>();
        ResetHealth();
    }

    public void TakeDamage(int damage)
    {
        healthBar.value -= damage;
        if (healthBar.value < 0)
        {
            healthBar.value = 0;
        }
    }

    public void HealDamage(int damage)
    {
        healthBar.value += damage;
        if (healthBar.value > 100)
        {
            healthBar.value = 100;
        }
    }

    public void ResetHealth()
    {
        healthBar.value = 100;
    }

    public void OnHealthValue_Changed()
    {
        value = (int)healthBar.value;
    }
}
