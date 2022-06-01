using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private ScoreScriptableObject highScore;
    [SerializeField] private TextMeshProUGUI highScoreNumText;

    // Start is called before the first frame update
    void Start()
    {
        highScore.Value = PlayerPrefs.GetFloat("HighScore"); // Get saved high score
        highScoreNumText.text = highScore.Value.ToString(); // Display high score
    }

    
    public void StartGame()
    {
        GameStateManager.Instance.StartGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetScore()
    {
        highScore.Value = 0f;
        PlayerPrefs.SetFloat("HighScore", 0f);
        highScoreNumText.text = highScore.Value.ToString();
    }
}
