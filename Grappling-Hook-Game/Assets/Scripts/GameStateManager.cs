using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public enum State { play, fail, pause, menu } // Enum to hold states of game


    private static GameStateManager _instance; // Holds the singleton instance of script

    private State state; // Holds current state

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
        LightProbes.TetrahedralizeAsync();
        state = State.play;
    }

    public void OnFail() // Called when player falls to death.
    {
        state = State.fail;
        ResetLevel();
    }

    public void ResetLevel() // Reset scene
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
