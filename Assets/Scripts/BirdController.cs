using UnityEngine;

public class PlayerMovementWithShooting : MonoBehaviour
{
    public float maxSpeed = 5f;        // Maximum movement speed
    public float acceleration = 10f;  // How quickly the object accelerates
    public float deceleration = 10f;  // How quickly the object decelerates when no key is pressed

    public GameObject projectilePrefab; // Prefab for the projectile
    public Transform firePoint;        // Point from which the projectile is fired
    public float projectileSpeed = 10f; // Speed of the projectile
    public float rapidFireRate = 0.2f;  // Time interval between shots in rapid-fire mode

    private float currentSpeed = 0f;    // Current speed of the object
    private float sKeyHoldTime = 0f;    // Time the S key has been held
    private bool isRapidFiring = false; // Whether the player is in rapid-fire mode

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    void HandleMovement()
    {
        // Input direction (-1 for A, 1 for D, 0 for no input)
        float targetDirection = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            targetDirection = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            targetDirection = 1f;
        }

        // Target speed based on input
        float targetSpeed = targetDirection * maxSpeed;

        // Accelerate or decelerate towards the target speed
        if (targetSpeed != 0)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
        }

        // Apply movement
        transform.Translate(new Vector3(currentSpeed * Time.deltaTime, 0f, 0f));
    }

    void HandleShooting()
    {
        if (Input.GetKey(KeyCode.S))
        {
            // Increase hold time for S key
            sKeyHoldTime += Time.deltaTime;

            // Activate rapid-fire if held for 2 seconds
            if (sKeyHoldTime >= 2f && !isRapidFiring)
            {
                isRapidFiring = true;
                InvokeRepeating(nameof(Shoot), 0f, rapidFireRate);
            }
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            // Reset hold time
            if (sKeyHoldTime < 2f)
            {
                Shoot(); // Tap shooting
            }

            sKeyHoldTime = 0f;

            // Stop rapid-fire mode
            if (isRapidFiring)
            {
                isRapidFiring = false;
                CancelInvoke(nameof(Shoot));
            }
        }
    }

    void Shoot()
    {
        // Instantiate projectile
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.gravityScale = 0f; // Disable gravity for the projectile
                rb.velocity = new Vector2(0f, projectileSpeed); // Move projectile upward
            }

            // Destroy the projectile after 5 seconds
            Destroy(projectile, 5f);
        }
    }
}
