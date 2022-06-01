using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScoreDisplay : MonoBehaviour
{

    [SerializeField] private ScoreScriptableObject currentScore;
    [SerializeField] private ScoreScriptableObject highScore;

    [SerializeField] TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Current Score: " + currentScore.Value.ToString();
    }
}
