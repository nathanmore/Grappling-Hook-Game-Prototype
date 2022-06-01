using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private ScoreScriptableObject highScore;
    [SerializeField] private TextMeshProUGUI highScoreNumText;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private TextMeshProUGUI sensitivityNumText;

    // Start is called before the first frame update
    void Start()
    {
        highScore.Value = PlayerPrefs.GetFloat("HighScore"); // Get saved high score
        highScoreNumText.text = highScore.Value.ToString(); // Display high score

        if (PlayerPrefs.HasKey("sensitivity") == true) // If data is saved, retrieve that
        {
            sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity");
        }
        else // else use default value
        {
            sensitivitySlider.value = 50f;
        }
        sensitivityNumText.text = sensitivitySlider.value.ToString(); // Set num text
    }

    public void StartGame()
    {
        GameStateManager.Instance.StartGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Options()
    {
        optionsPanel.SetActive(true);
    }

    public void ExitOptions()
    {
        optionsPanel.SetActive(false);
    }

    public void SliderChange(float newValue)
    {
        sensitivityNumText.text = newValue.ToString();
        PlayerPrefs.SetFloat("sensitivity", newValue);
    }

    public void ResetScore()
    {
        highScore.Value = 0f;
        PlayerPrefs.SetFloat("HighScore", 0f);
        highScoreNumText.text = highScore.Value.ToString();
    }
}
