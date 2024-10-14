using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public int[] buttonCost;
    public Button[] storeButton;
    
    public void CheckPlayerCoins(int coins)
    {
        for (int i = 0; i < storeButton.Length; i++) 
        {
            storeButton[i].interactable = coins > buttonCost[i];
        }
    }
}
