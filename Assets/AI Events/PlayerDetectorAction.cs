using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PlayerDetector", story: "[Agents] Detects [Player]", category: "Action", id: "43a36f2a4d55ca4feb2d313f1665d3f3")]
public partial class PlayerDetectorAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agents;
    [SerializeReference] public BlackboardVariable<GameObject> Player;

    private NavMeshAgent agent;
    private PlayerSensor sensor;


    protected override Status OnStart()
    {
        agent = Agents.Value.GetComponent<NavMeshAgent>();
        sensor = Agents.Value.GetComponent<PlayerSensor>();

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        var target = sensor.CurrentTarget;
        if (target == null ) return Status.Running;

        Player.Value = target.gameObject;
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

