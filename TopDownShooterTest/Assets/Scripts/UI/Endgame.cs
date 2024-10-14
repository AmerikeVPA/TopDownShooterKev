using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Endgame : MonoBehaviour
{
    public TextMeshProUGUI scoreText, waveText;

    public void SetTexts(int score, int wave)
    {
        scoreText.text = $"Final Score: {score}";
        waveText.text = $"Waves: {wave}";
    }
}
