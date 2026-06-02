using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RepositionLeft", story: "[Agent] finds [Waypoint] to the left [X] away", category: "Action", id: "e560fac5c28dc539ef3adfa6298af773")]
public partial class RepositionLeftAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Waypoint;
    [SerializeReference] public BlackboardVariable<float> X;
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

