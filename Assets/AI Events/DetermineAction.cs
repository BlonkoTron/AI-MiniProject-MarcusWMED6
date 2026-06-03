using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DetermineAction", story: "[Agent] decides [Action] based on distance to [Target] Based on [Weights]", category: "Action", id: "2f310a221cf002066d9df7623435cdbb")]
public partial class DetermineAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<ChosenAction> Action;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<ActionWeights> Weights;

    private bool firstLoad = true;

    protected override Status OnStart()
    {
        weights = Weights.Value;

        if (firstLoad)
        {
            LoadWeightsFromScriptableObject();
        }

        Action.Value = DetermineNextState();

        Debug.Log(Action.Value);

        return Status.Success;
    }

    private ActionWeights weights;

    private float closeRange = 5f;
    private float mediumRange = 10f;

    [Header("State Weights when CLOSE (< Close Range)")]
    private float closeRepositionWeight = 70f;
    private float closeAttackWeight = 25f;
    private float closeShootWeight = 5f;

    [Header("State Weights when MEDIUM (Between Close and Medium)")]
    private float medRepositionWeight = 20f;
    private float medAttackWeight = 65f;
    private float medShootWeight = 15f;

    [Header("State Weights when FAR (> Medium Range)")]
    private float farRepositionWeight = 10f;
    private float farAttackWeight = 10f;
    private float farShootWeight = 80f;

    public void LoadWeightsFromScriptableObject()
    {
        if (weights == null)
        {
            Debug.LogError("Weights ScriptableObject reference is missing!");
            return;
        }

        // Assigning the local variables from the ScriptableObject values
        closeRange = weights.closeRange;
        mediumRange = weights.mediumRange;

        // Loading Close weights
        closeRepositionWeight = weights.closeRepositionWeight;
        closeAttackWeight = weights.closeAttackWeight;
        closeShootWeight = weights.closeShootWeight;

        // Loading Medium weights
        medRepositionWeight = weights.medRepositionWeight;
        medAttackWeight = weights.medAttackWeight;
        medShootWeight = weights.medShootWeight;

        // Loading Far weights
        farRepositionWeight = weights.farRepositionWeight;
        farAttackWeight = weights.farAttackWeight;
        farShootWeight = weights.farShootWeight;

        Debug.Log("Local enemy weights successfully updated from the Weights ScriptableObject!");

        firstLoad = false;

    }


    public ChosenAction DetermineNextState()
    {
        float distance = Vector3.Distance(Agent.Value.transform.position, Target.Value.transform.position);

        float wReposition = 0f;
        float wAttack = 0f;
        float wShoot = 0f;

        //Determine weights based on distance zone
        if (distance < closeRange)
        {
            wReposition = closeRepositionWeight;
            wAttack = closeAttackWeight;
            wShoot = closeShootWeight;
        }
        else if (distance <= mediumRange)
        {
            wReposition = medRepositionWeight;
            wAttack = medAttackWeight;
            wShoot = medShootWeight;
        }
        else // distance > mediumRange (This is far range)
        {
            wReposition = farRepositionWeight;
            wAttack = farAttackWeight;
            wShoot = farShootWeight;
        }

        
        return ChooseWeightedState(wReposition, wAttack, wShoot);

    }
    
    private ChosenAction ChooseWeightedState(float repoW, float attackW, float shootW)
    {
        float totalWeight = repoW + attackW + shootW;
        float randomValue = UnityEngine.Random.Range(0f, totalWeight);

        if (randomValue < repoW)
        {
            Debug.Log(randomValue + "ChosenAction.Repos");
            return ChosenAction.Repos;
            
        }
        else if (randomValue < repoW + attackW)
        {
            Debug.Log(randomValue + "ChosenAction.Attack");
            return ChosenAction.Attack;
        }
        else
        {
            Debug.Log(randomValue + "ChosenAction.Shoot");
            return ChosenAction.Shoot;
        }
    }

}

