using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text healthBarText;

    Damageable playerDamageable;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerDamageable = player.GetComponent<Damageable>();
    }

    // Start is called before the first frame update
    void Start()
    {      
        healthSlider.value = CalculateHealthPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        healthBarText.text = "Vida " + playerDamageable.Health + " / " + playerDamageable.MaxHealth;
    }

    public float CalculateHealthPercentage(float currentHealth, float maxHealt)
    {
        return currentHealth / maxHealt;
    }

    private void OnEnable()
    {
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChange);
    }
    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChange);

    }

    private void OnPlayerHealthChange(int newHealth, int maxHealth)
    {
        healthSlider.value = CalculateHealthPercentage(newHealth, maxHealth);
        healthBarText.text = "Vida " + newHealth + " / " + maxHealth;
    }
}
