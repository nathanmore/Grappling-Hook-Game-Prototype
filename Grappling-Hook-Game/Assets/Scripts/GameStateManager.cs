using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public enum State { play, fail, pause, menu } // Enum to hold states of game

    [SerializeField] private ScoreScriptableObject currentScore;
    [SerializeField] private ScoreScriptableObject highScore;


    private static GameStateManager _instance; // Holds the singleton instance of script

    public State state; // Holds current state

    public static GameStateManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameStateManager>();
            }

            return _instance;
        }
    } // Manages current instance of singleton

    public void Awake()
    {
        if (_instance == null) // Sets new instance of script
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
        else // Destroys new instances of script if one already exists
        {
            Destroy(this);
        }
    }

    public void Start()
    {
        state = State.menu;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("main");
        state = State.play;
    }

    public void PauseGame()
    {
        if (state == State.pause)
        {
            SceneManager.UnloadSceneAsync("PauseMenu");
            state = State.play;
            Time.timeScale = 1;

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
        else if (state == State.play)
        {
            Time.timeScale = 0;
            state = State.pause;
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void OnFail() // Called when player falls to death.
    {
        Time.timeScale = 0;
        state = State.fail;
        UpdateHighScore();
        SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResetLevel() // Reset scene
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        state = State.play;
        Time.timeScale = 1;
    }

    public void UpdateHighScore()
    {
        if (currentScore.Value > highScore.Value)
        {
            highScore.Value = currentScore.Value;
        }
    }
}
