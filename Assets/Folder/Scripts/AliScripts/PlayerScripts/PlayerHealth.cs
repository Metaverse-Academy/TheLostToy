using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public HeartbeatSound heartbeatSound;
    private float currentHealth;
    private GameManager gameManager;
    void Start()
    {
        currentHealth = maxHealth;
        gameManager = FindObjectOfType<GameManager>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= (float)damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Update()
    {
        if (heartbeatSound != null)
        {
            heartbeatSound.UpdateHeartbeat(currentHealth);
        }
    }
    void Die()
    {
        Destroy(gameObject);
        Debug.Log("Player Died");
        if (gameManager != null)
        {
            gameManager.GameOver();
        }
    }
}