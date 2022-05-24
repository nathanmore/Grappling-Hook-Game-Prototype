using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] private ScoreScriptableObject currentScore;
    [SerializeField] private Transform playerTransform;

    void Update()
    {
        if (GameStateManager.Instance.state == GameStateManager.State.play)
        {
            currentScore.Value = CalculateDistance();
        }
    }

    public float CalculateDistance()
    {
        float distanceTravelled = playerTransform.position.z - gameObject.transform.position.z;
        
        return Mathf.Round(distanceTravelled);
    }
}
