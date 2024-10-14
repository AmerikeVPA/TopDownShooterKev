using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI specialAmmoCounter, waveCounter, coinCounter;
    public Slider healthBar, shieldBar;

    public void ShowAmmoCounter(int ammoAvailable, int maxAmmo)
    {
        specialAmmoCounter.text = $"Missiles: {ammoAvailable}/{maxAmmo}";
    }
    public void ChangeBarValue(bool useHealth, float maxValue, float currentValue)
    {
        Slider barToUse = useHealth ? healthBar : shieldBar;
        barToUse.value = currentValue / maxValue;
    }
    public void ShowCoinCount(int coinsAvailable)
    {
        coinCounter.text = $"Coins: {coinsAvailable}";
    }
    public void ShowWaveCount(int wave)
    {
        waveCounter.text = wave.ToString();
    }
}
