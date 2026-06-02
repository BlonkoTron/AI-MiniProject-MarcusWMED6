using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetReposition", story: "[Agent] grabs a [waypoint] [X] units behind it", category: "Action", id: "3524db808575b17c680e5814cfaa6bce")]
public partial class GetRepositionAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Waypoint;
    [SerializeReference] public BlackboardVariable<float> X;
    protected override Status OnStart()
    {

        Vector3 RepositionVector = GetPositionBehind(X);
        Waypoint.Value.transform.position = RepositionVector;

        return Status.Success;
    }

    public Vector3 GetPositionBehind(float distance)
    {
        // Start at the object's current position, then subtract its forward direction multiplied by the distance
        return Agent.Value.transform.position - (Agent.Value.transform.forward * distance);
    }

}

