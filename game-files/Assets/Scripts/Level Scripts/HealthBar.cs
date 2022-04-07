using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public IVBar ivBar;

    public LevelManager levelManager;

    // Patients' Health
    [HideInInspector] public int maxHealth;
    [HideInInspector] public int currentHealth;

    void Start()
    {
        maxHealth = levelManager.sufficientHealth;
        currentHealth = levelManager.startingHealth;
        SetMaxHealth(maxHealth);
        SetHealth(currentHealth);
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
