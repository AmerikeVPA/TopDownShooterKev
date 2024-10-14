using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomySystem : MonoBehaviour
{
    public UIController uIController;

    public void AddCoin()
    {
        uIController.coins++;
    }    
}
