using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TMP_Text scoreText;

    void Update()
    {
        if (GameManager.GM.gameState == GameState.Playing)
        {
            scoreText.text = "Score : " + GameManager.GM.CalculateScore();
        }
    }
}
