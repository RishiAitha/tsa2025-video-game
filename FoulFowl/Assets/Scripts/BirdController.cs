using UnityEngine;
using UnityEngine.UI;

public class BirdController : MonoBehaviour
{
    public int playerNum; // Player number

    public float rotationSpeed = 100f; // Speed at which the bird rotates
    public float upwardSpeed = 0f;    // Speed at which the bird moves upward
    public float maxUpwardSpeed = 5f; // Maxmium speed at which the bird moves forward
    public float recoilSpeed = -3f;

    public GameObject featherPrefab; // Prefab for the feather projectile
    public GameObject eggPrefab;    // Prefab for the egg projectile
    public Transform firePoint;        // Point from which the projectile is fired
    public float projectileSpeed = 10f; // Speed of the projectile
    public float rapidFireRate = 0.2f;  // Time interval between shots in rapid-fire mode

    private float sKeyHoldTime = 0f;    // Time the S key has been held
    public float eggShotTime = 3f;      // Time needed to shoot an egg
    public Image eggBar;                // Bar used to show time neeeded for egg shots

    public KeyCode leftRotationKey;
    public KeyCode rightRotationKey;
    public KeyCode shootingKey;

    public float health = 100f;

    void Update()
    {
        HandleRotation();
        HandleShooting();
        MoveUpward();

        if (health <= 0f)
        {
            gameObject.SetActive(false);
        }
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
        eggBar.fillAmount = sKeyHoldTime / eggShotTime;

        if (Input.GetKey(shootingKey))
        {
            // Increase hold time for S key
            sKeyHoldTime += Time.deltaTime;
        }

        if (Input.GetKeyUp(shootingKey))
        {
            Shoot(sKeyHoldTime >= eggShotTime);

            sKeyHoldTime = 0f;
        }
    }

    void Shoot(bool isEggShot)
    {
        // Instantiate projectile
        if (featherPrefab != null && firePoint != null && eggPrefab != null)
        {
            GameObject projectile;
            if (isEggShot)
            {
                projectile = Instantiate(eggPrefab, firePoint.position, firePoint.rotation);
            }
            else
            {
                projectile = Instantiate(featherPrefab, firePoint.position, firePoint.rotation);
            }

            projectile.GetComponent<BirdProjectileController>().correspondingPlayer = playerNum;

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

    void ReverseOtherControls()
    {
        Debug.Log("reverse controls here");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BossProjectile")
        {
            health -= 5f;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "ReverseProjectile")
        {
            ReverseOtherControls();
            Destroy(collision.gameObject);
        }
    }
}
