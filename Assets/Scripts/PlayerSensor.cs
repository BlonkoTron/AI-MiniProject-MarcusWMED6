using UnityEngine;

public class PlayerSensor : MonoBehaviour
{
    // This property allows other scripts to read the target
    public Transform CurrentTarget { get; private set; }

    [Header("Detection Settings")]
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private LayerMask targetLayer; // Set this to your Player layer

    private void Update()
    {
        DetectTarget();
    }

    private void DetectTarget()
    {
        // Creates an invisible sphere check. Returns an array of all colliders inside it.
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, targetLayer);

        if (colliders.Length > 0)
        {
            // Assign the first object found in the layer mask as the target
            CurrentTarget = colliders[0].transform;
        }
        else
        {
            // Clear the target if nothing from that layer is inside the radius
            CurrentTarget = null;
        }
    }

    private float GetDistanceToTarget()
    {
        if (CurrentTarget != null)
        {
            return Vector3.Distance(gameObject.transform.position, CurrentTarget.transform.position);
        }
        else return 0;

    }

    // Draws a wireframe sphere in the Unity Editor scene view so you can visualize the range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
