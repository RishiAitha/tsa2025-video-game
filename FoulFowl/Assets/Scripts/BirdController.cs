using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float rotationSpeed = 100f; // Speed at which the bird rotates
    public float upwardSpeed = 0f;    // Speed at which the bird moves upward
    public float maxUpwardSpeed = 5f; // Maxmium speed at which the bird moves forward
    public float recoilSpeed = -3f;

    public GameObject projectilePrefab; // Prefab for the projectile
    public Transform firePoint;        // Point from which the projectile is fired
    public float projectileSpeed = 10f; // Speed of the projectile
    public float rapidFireRate = 0.2f;  // Time interval between shots in rapid-fire mode

    private float sKeyHoldTime = 0f;    // Time the S key has been held
    private bool isRapidFiring = false; // Whether the bird is in rapid-fire mode

    public KeyCode leftRotationKey;
    public KeyCode rightRotationKey;
    public KeyCode shootingKey;

    void Update()
    {
        HandleRotation();
        HandleShooting();
        MoveUpward();
    }

    void HandleRotation()
    {
        float rotationDirection = 0f;

        // Check for rotation input
        if (Input.GetKey(leftRotationKey))
        {
            rotationDirection = 1f; // Rotate counterclockwise
        }
        else if (Input.GetKey(rightRotationKey))
        {
            rotationDirection = -1f; // Rotate clockwise
        }

        // Apply rotation
        transform.Rotate(new Vector3(0f, 0f, rotationDirection * rotationSpeed * Time.deltaTime));
    }

    void MoveUpward()
    {
        // Accelerate bird speed towards maximum
        if (upwardSpeed < maxUpwardSpeed)
        {
            upwardSpeed += 0.1f;
        }

        // Move forward in the direction the bird is facing
        Vector3 forwardDirection = transform.up.normalized; // Bird's forward direction
        transform.Translate(forwardDirection * upwardSpeed * Time.deltaTime, Space.World);
    }


    void HandleShooting()
    {
        if (Input.GetKey(shootingKey))
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

        if (Input.GetKeyUp(shootingKey))
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
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.gravityScale = 0f; // Disable gravity for the projectile
                rb.velocity = firePoint.up * projectileSpeed; // Shoot projectile in the bird's forward (upward) direction
            }

            // Destroy the projectile after 5 seconds
            Destroy(projectile, 5f);

            // Trigger recoil after shooting
            upwardSpeed = recoilSpeed;
        }
    }
}
