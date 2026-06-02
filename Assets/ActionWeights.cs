using UnityEngine;

[CreateAssetMenu(fileName = "ActionWeights", menuName = "Scriptable Objects/ActionWeights")]
public class ActionWeights : ScriptableObject
{
    [Header("Distance Thresholds")]
    public float closeRange = 3f;
    public float mediumRange = 10f;

    [Header("State Weights when CLOSE (< Close Range)")]
    public float closeRepositionWeight = 70f;
    public float closeAttackWeight = 25f;
    public float closeShootWeight = 5f;

    [Header("State Weights when MEDIUM (Between Close and Medium)")]
    public float medRepositionWeight = 20f;
    public float medAttackWeight = 65f;
    public float medShootWeight = 15f;

    [Header("State Weights when FAR (> Medium Range)")]
    public float farRepositionWeight = 10f;
    public float farAttackWeight = 10f;
    public float farShootWeight = 80f;
}
