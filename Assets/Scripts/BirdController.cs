using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float maxSpeed = 5f;        // Maximum movement speed
    public float acceleration = 10f;  // How quickly the bird accelerates
    public float deceleration = 10f;  // How quickly the bird decelerates when no key is pressed

    public GameObject projectilePrefab; // Prefab for the projectile
    public Transform firePoint;        // Point from which the projectile is fired
    public float projectileSpeed = 10f; // Speed of the projectile
    public float rapidFireRate = 0.2f;  // Time interval between shots in rapid-fire mode

    private float currentSpeed = 0f;    // Current speed of the bird
    private float sKeyHoldTime = 0f;    // Time the S key has been held
    private bool isRapidFiring = false; // Whether the bird is in rapid-fire mode

    public KeyCode leftMovement;
    public KeyCode rightMovement;
    public KeyCode shootingKey;

    public float upwardSpeed = 1f;       // Speed at which the bird moves upward
    public float recoilAmount = 0.5f;    // How far the bird moves downward when shooting
    public float recoilDuration = 0.2f;  // Time over which the recoil happens
    private bool isRecoiling = false;    // To prevent multiple recoil effects overlapping

    void Update()
    {
        HandleMovement();
        HandleShooting();
        MoveUpward();
    }

    void HandleMovement()
    {
        // Input direction (-1 for A, 1 for D, 0 for no input)
        float targetDirection = 0f;

        if (Input.GetKey(leftMovement))
        {
            targetDirection = -1f;
        }
        else if (Input.GetKey(rightMovement))
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

        // Apply horizontal movement
        transform.Translate(new Vector3(currentSpeed * Time.deltaTime, 0f, 0f));
    }

    void MoveUpward()
    {
        // Slowly move upward unless recoiling
        if (!isRecoiling)
        {
            transform.Translate(new Vector3(0f, upwardSpeed * Time.deltaTime, 0f));
        }
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
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.gravityScale = 0f; // Disable gravity for the projectile
                rb.velocity = new Vector2(0f, projectileSpeed); // Move projectile upward
            }

            // Destroy the projectile after 5 seconds
            Destroy(projectile, 5f);

            // Trigger recoil after shooting
            if (!isRecoiling)
            {
                StartCoroutine(HandleRecoil());
            }
        }
    }

    private System.Collections.IEnumerator HandleRecoil()
    {
        isRecoiling = true;

        float elapsedTime = 0f;
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y - recoilAmount, transform.position.z);

        // Smoothly move downward over the recoil duration
        while (elapsedTime < recoilDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / recoilDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition; // Ensure final position is exact
        isRecoiling = false;
    }
}
