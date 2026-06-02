using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RepositionRight", story: "[Agent] Finds [Waypoint] to the right [X] units Away", category: "Action", id: "3a777287fca3874bf658db14cfd85f26")]
public partial class RepositionRightAction : Action
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

