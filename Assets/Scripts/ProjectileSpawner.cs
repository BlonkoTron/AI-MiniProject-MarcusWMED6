using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject projectile;

    private void OnEnable()
    {
        Instantiate(projectile, transform.position, transform.rotation);
    }
}
