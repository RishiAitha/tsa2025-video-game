using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject projectilePrefab; // The projectile prefab
    public float projectileSpeed = 5f;  // Speed of the projectiles
    public int numberOfProjectiles = 12; // Number of projectiles to shoot out evenly
    public float fireRate = 2f;         // Time interval between each burst of projectiles
    public float projectileLifetime = 3f; // Time before projectiles are destroyed

    private float timeSinceLastFire;

    void Update()
    {
        // Track time and fire projectiles at regular intervals
        timeSinceLastFire += Time.deltaTime;

        if (timeSinceLastFire >= fireRate)
        {
            ShootProjectiles();
            timeSinceLastFire = 0f;
        }
    }

    void ShootProjectiles()
    {
        // Calculate the angle between each projectile
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            // Calculate the direction for the current projectile
            float projectileDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float projectileDirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 projectileDirection = new Vector2(projectileDirX, projectileDirY).normalized;

            // Spawn the projectile
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // Rotate the projectile to match its direction
            float rotationZ = Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + 90f);

            // Apply velocity to the projectile
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0f; // Disable gravity for the projectile
                rb.velocity = projectileDirection * projectileSpeed; // Move the projectile in the calculated direction
            }

            // Destroy the projectile after a set time
            Destroy(projectile, projectileLifetime);

            // Increment angle for the next projectile
            angle += angleStep;
        }
    }
}
