using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private ScoreScriptableObject currentScore;
    [SerializeField] private ScoreScriptableObject highScore;
    [SerializeField] private TextMeshProUGUI yourScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        yourScoreText.text = "Your Score: " + currentScore.Value.ToString();
        highScoreText.text = "High Score: " + highScore.Value.ToString();
    }

    public void RestartGame()
    {
        GameStateManager.Instance.ResetLevel();
    }

    public void MainMenu()
    {
        GameStateManager.Instance.OpenMainMenu();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
