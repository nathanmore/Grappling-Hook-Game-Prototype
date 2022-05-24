using UnityEngine;

[CreateAssetMenu(fileName = "Score", menuName = "ScriptableObjects/Score")]
public class ScoreScriptableObject : ScriptableObject
{
    [SerializeField] private float floatValue;

    public float Value { get { return floatValue; } set { floatValue = value; } }
}
