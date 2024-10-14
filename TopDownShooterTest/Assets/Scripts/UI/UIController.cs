using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject HUD, store, gameOver;
    public TextMeshProUGUI scoreText;

    [HideInInspector]
    public int coins = 0;
    private Store storeComponent;

    private void Awake()
    {
        storeComponent = store.GetComponent<Store>();
    }
    public void ShowStore()
    {
        HUD.SetActive(false);
        store.SetActive(true);
        storeComponent.CheckPlayerCoins(coins);
    }
    public void ResumeGame()
    {
        HUD.SetActive(true);
        store.SetActive(false);
    }
    public void ShowGameOver(int score)
    {
        HUD.SetActive(false);
        gameOver.SetActive(true);
        scoreText.text = $"Your Score: {score}";
    }
}
