using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckIfReachable", story: "Checks if [Target] is reachable by [Agent]", category: "Action", id: "229018d33d3aeb1543ae1a2fb67df81e")]
public partial class CheckIfReachableAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<GameObject> Agent;

    private NavMeshAgent navAgent;
    private NavMeshPath m_Path;
    protected override Status OnStart()
    {

        navAgent = Agent.Value.GetComponent<NavMeshAgent>();

        if (Agent == null)
        {
            LogFailure("Agent is null on the Blackboard.");
            return Status.Failure;
        }

        if (navAgent == null)
        {
            LogFailure("Agent (NavMeshAgent) is null on the Blackboard.");
            return Status.Failure;
        }

        if (Target.Value == null)
        {
            LogFailure("Target GameObject is null on the Blackboard.");
            return Status.Failure;
        }

        m_Path = new NavMeshPath();
        Vector3 targetPos = Target.Value.transform.position;

        // 2. Step 1: Check if the target's position is actually on/near the NavMesh boundaries
        NavMeshHit hit;
        if (!NavMesh.SamplePosition(targetPos, out hit, 2f, NavMesh.AllAreas))
        {
            LogFailure($"Target '{Target.Value.name}' is completely outside NavMesh boundaries.");
            return Status.Failure;
        }

        // 3. Step 2: Calculate if a complete path exists to that snapped position
        navAgent.CalculatePath(hit.position, m_Path);

        if (m_Path.status == NavMeshPathStatus.PathInvalid)
        {
            LogFailure("Calculated path is invalid.");
            return Status.Failure;
        }
        else if (m_Path.status == NavMeshPathStatus.PathPartial)
        {
            LogFailure($"Calculated path to '{Target.Value.name}' is partial (target is unreachable/blocked).");
            return Status.Failure;
        }

        // 4. Step 3: If valid, apply the pre-calculated path to the agent
        navAgent.SetPath(m_Path);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Agent.Value == null) return Status.Failure;

        // Check if the agent has successfully arrived at the destination
        if (!navAgent.pathPending)
        {
            if (navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
                if (!navAgent.hasPath || navAgent.velocity.sqrMagnitude == 0f)
                {
                    return Status.Success;
                }
            }
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
        // Clean up the agent's path when the node exits or gets interrupted
        if (Agent.Value != null && navAgent.isOnNavMesh)
        {
            navAgent.ResetPath();
        }
    }
}

