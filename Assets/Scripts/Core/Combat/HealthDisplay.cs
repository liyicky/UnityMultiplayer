using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private Health health;
    [SerializeField] private Image healthBarImage;

    public override void OnNetworkSpawn()
    {
        if (!IsClient) { return; }
        health.currentHealth.OnValueChanged += HandleHealthChanged;
        HandleHealthChanged(0, health.currentHealth.Value);
    }

    public override void OnNetworkDespawn()
    {
        if (!IsClient) { return; }
        health.currentHealth.OnValueChanged -= HandleHealthChanged;
    }

    private void HandleHealthChanged(int oldHealth, int newHealth)
    {
        float healthPercentage = (float) newHealth / health.maxHealth;
        Debug.Log("Health Percentage: " + healthPercentage);
        healthBarImage.fillAmount = healthPercentage;
    }
}
