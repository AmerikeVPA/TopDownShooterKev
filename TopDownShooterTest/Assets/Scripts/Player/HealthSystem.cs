using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float regenTreshold = 4.5f, regenSpeed = 1.25f;
    public Health shield, health;
    public HUD gameHud;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        CreateHealthClasses();
        gameHud.ChangeBarValue(true, health.maxHealth, health.currentHealth);
        gameHud.ChangeBarValue(false, shield.maxHealth, shield.currentHealth);
    }
    private void CreateHealthClasses()
    {
        shield = new Health(100, 100);
        health = new Health(100, 100);
    }
    public  void TakeDamage(float damage)
    {
        InterruptRegen();
        StartCoroutine(RegenCountdown());
        if (shield.ReduceHealth(damage))
        {
            gameHud.ChangeBarValue(false, shield.maxHealth, shield.currentHealth);
            return;
        }
        if (health.ReduceHealth(damage))
        {
            gameHud.ChangeBarValue(true, health.maxHealth, health.currentHealth);
            return;
        }
        gameManager.GameOver();
    }

    #region Upgrades
    public void UpgradeStat(bool useHealth)
    {
        Health healthToIncrease = useHealth ? health : shield;
        healthToIncrease.UpgradeMaxHealth(10);
        gameHud.ChangeBarValue(useHealth, healthToIncrease.maxHealth, healthToIncrease.currentHealth);
    }
    public void RefillHealth()
    {
        health.ResetHealth();
        gameHud.ChangeBarValue(true, health.maxHealth, health.currentHealth);
    }
    public void UpgradeShieldRegen()
    {
        regenTreshold -= .25f;
    }
    #endregion

    #region Shield Regeneration
    private IEnumerator RegenCountdown()
    {
        yield return new WaitForSeconds(regenTreshold);
        StartCoroutine(RegenShield());
    }
    private IEnumerator RegenShield()
    {
        yield return new WaitForEndOfFrame();
        if (shield.currentHealth <= shield.maxHealth)
        {
            shield.ResetHealth();
            gameHud.ChangeBarValue(false, shield.maxHealth, shield.currentHealth);
            StopCoroutine(RegenShield());
        }
        shield.currentHealth += Time.deltaTime;
        gameHud.ChangeBarValue(false, shield.maxHealth, shield.currentHealth);
        StartCoroutine(RegenShield());
    }
    private void InterruptRegen()
    {
        StopAllCoroutines();
    }
    #endregion
}
