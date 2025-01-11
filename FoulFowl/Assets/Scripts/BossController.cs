using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class BossController : MonoBehaviour
{
    public GameObject projectilePrefab; // The projectile prefab
    public GameObject reverseProjectilePrefab; // Special control-reversing projectile prefab
    public GameObject upgradeProjectilePrefab;
    public float projectileSpeed = 5f;  // Speed of the projectiles
    public int numberOfProjectiles = 5; // Number of projectiles to shoot out evenly
    public float fireRate = 2f;         // Time interval between each burst of projectiles
    public float projectileLifetime = 3f; // Time before projectiles are destroyed

    private float timeSinceLastFire;

    public float health = 1000f;
    public PlayerManager playerManager;

    public GameObject winMenu;
    public GameObject pauseMenu;
    public TextMeshProUGUI winnerText;

    public float reverseProjectileChance = 0.005f;
    public float upgradeProjectileChance = 0.005f;

    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        // Track time and fire projectiles at regular intervals
        timeSinceLastFire += Time.deltaTime;

        if (timeSinceLastFire >= fireRate)
        {
            ShootProjectiles();
            timeSinceLastFire = 0f;
        }

        if (health <= 0f)
        {
            Win();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }

    void ShootProjectiles()
{
    // List to store the used angles
    List<float> usedAngles = new List<float>();
    int maxAttempts = 10; // Prevent infinite loops when generating random angles

    for (int i = 0; i < numberOfProjectiles; i++)
    {
        float angle = 0f;
        int attempts = 0;
        bool isValidAngle;

        // Generate a random angle that is not within 10 degrees of any used angle
        do
        {
            angle = Random.Range(0f, 360f);
            isValidAngle = true;

            foreach (float usedAngle in usedAngles)
            {
                if (Mathf.Abs(Mathf.DeltaAngle(angle, usedAngle)) < 10f) // Check if angle is within 10 degrees
                {
                    isValidAngle = false;
                    break;
                }
            }

            attempts++;
        } while (!isValidAngle && attempts < maxAttempts);

        if (attempts >= maxAttempts)
        {
            Debug.LogWarning("Could not find a valid angle. Reducing number of projectiles.");
            break;
        }

        usedAngles.Add(angle);

        // Calculate the direction for the projectile
        float projectileDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
        float projectileDirY = Mathf.Sin(angle * Mathf.Deg2Rad);
        Vector2 projectileDirection = new Vector2(projectileDirX, projectileDirY).normalized;

        GameObject projectile;
        bool isReverseProjectile;

        // Spawn the projectile
        if (Random.Range(0f, 1f) <= reverseProjectileChance)
        {
            projectile = Instantiate(reverseProjectilePrefab, transform.position, Quaternion.identity);
            isReverseProjectile = true;
        }
        else if (Random.Range(0f, 1f) <= upgradeProjectileChance)
        {
            projectile = Instantiate(upgradeProjectilePrefab, transform.position, Quaternion.identity);
            isReverseProjectile = false;
        }
        else
        {
            projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            isReverseProjectile = false;
        }

        // Rotate the projectile to match its direction
        float rotationZ = Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + 90f);

        // Apply velocity to the projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0f; // Disable gravity for the projectile
            if (isReverseProjectile)
            {
                rb.velocity = projectileDirection * projectileSpeed / 2; // Move the projectile in the calculated direction
            }
            else
            {
                rb.velocity = projectileDirection * projectileSpeed; // Move the projectile in the calculated direction
            }
        }

        // Destroy the projectile after a set time
        Destroy(projectile, projectileLifetime);
    }
}


    void Win()
    {
        Time.timeScale = 0;
        int winningPlayer = -1;
        float maxDamage = 0;
        for (int i = 0; i < GameData.playerCount; i++)
        {
            if (playerManager.playerDamageList[i] >= maxDamage && playerManager.playerList[i].activeInHierarchy)
            {
                winningPlayer = i;
                maxDamage = playerManager.playerDamageList[i];
            }
        }

        switch (SceneManager.GetActiveScene().name)
        {
            case "LevelScene0":
                GameData.levelWinners[0] = winningPlayer;
                GameData.levelsPlayed[0] = true;
                break;
            case "LevelScene1":
                GameData.levelWinners[1] = winningPlayer;
                GameData.levelsPlayed[1] = true;
                break;
            case "LevelScene2":
                GameData.levelWinners[2] = winningPlayer;
                GameData.levelsPlayed[2] = true;
                break;
        }

        winnerText.text = "The foulest fowl is: Player " + (winningPlayer + 1) + "!";
        switch (winningPlayer)
        {
            case 0:
                winnerText.color = new Color(0f, 0f, 1f);
                break;
            case 1:
                winnerText.color = new Color(1f, 0f, 0f);
                break;
            case 2:
                winnerText.color = new Color(1f, 1f, 0f);
                break;
            case 3:
                winnerText.color = new Color(0f, 1f, 0f);
                break;
        }

        winMenu.SetActive(true);

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BirdProjectile")
        {
            BirdProjectileController projectileController = collision.gameObject.GetComponent<BirdProjectileController>();
            health -= projectileController.damage;
            playerManager.playerDamageList[projectileController.correspondingPlayer] += projectileController.damage;
            Destroy(collision.gameObject);
        }
    }
}
