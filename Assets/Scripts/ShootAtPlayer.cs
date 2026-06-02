using UnityEngine;

public class ShootAtPlayer : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 20f;
    [SerializeField] private string playerTag = "Player";

    [Header("Optional Effects")]
    [SerializeField] private bool rotateTowardsTarget = true;
    [SerializeField] private GameObject poofPrefab;

    private Rigidbody2D rb2d;
    private Rigidbody rb3d;

    void Start()
    {
        if (poofPrefab != null)
        {
            Instantiate(poofPrefab, transform.position, transform.rotation);
        }

        // 1. Find the player GameObject in the scene
        GameObject player = GameObject.FindWithTag(playerTag);

        if (player == null)
        {
            Debug.LogWarning($"Projectile could not find an object with the tag '{playerTag}'!");
            Destroy(gameObject); // Optional: Destroy itself if there's no player to shoot at
            return;
        }

        // 2. Calculate the direction vector from the projectile to the player
        Vector3 direction = (player.transform.position - transform.position).normalized;

        // 3. Apply velocity based on whether your game is 2D or 3D

        rb3d = GetComponent<Rigidbody>();

        if (rb3d != null)
        {
            rb3d.linearVelocity = direction * speed; // Use rb3d.velocity if using older Unity versions

            if (rotateTowardsTarget)
            {
                transform.forward = direction;
            }
        }
        else
        {
            // Fallback: If you aren't using physics/rigidbodies, use this fallback
            // Note: If using this fallback, you will need to add transform.Translate to Update()
            Debug.LogWarning("No Rigidbody found. Applying velocity purely via physics is recommended.");
        }

        // 4. Lifetime safeguard (so bullets don't fly into the void forever)
        Destroy(gameObject, 10f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Player");

            Destroy(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnDestroy()
    {
        if (poofPrefab != null)
        {
            Instantiate(poofPrefab, transform.position, transform.rotation);
        }
    }

}