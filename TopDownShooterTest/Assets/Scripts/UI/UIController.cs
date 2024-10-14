using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject HUD, store, gameOver;

    private int coins = 0;
    private HUD hud;
    private Store storeComponent;
    private Endgame gameoverUI;

    private void Awake()
    {
        hud = HUD.GetComponent<HUD>();
        storeComponent = store.GetComponent<Store>();
        gameoverUI = gameOver.GetComponent<Endgame>();

        hud.ShowCoinCount(coins);
    }
    public void ShowStore()
    {
        HUD.SetActive(false);
        store.SetActive(true);
        storeComponent.CheckPlayerCoins(coins);
    }
    public void ResumeGame(int nextWave)
    {
        HUD.SetActive(true);
        hud.ShowWaveCount(nextWave);
        store.SetActive(false);
    }
    public void ShowGameOver(int score, int finalWave)
    {
        HUD.SetActive(false);
        gameOver.SetActive(true);
        gameoverUI.SetTexts(score, finalWave);
    }
    public void AddCoin()
    {
        coins++;
        hud.ShowCoinCount(coins);
    }
}
