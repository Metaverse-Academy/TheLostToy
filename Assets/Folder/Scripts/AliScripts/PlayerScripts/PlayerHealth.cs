using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public HeartbeatSound heartbeatSound;
    private int currentHealth;
    private GameManager gameManager;
    void Start()
    {
        currentHealth = maxHealth;
        gameManager = FindObjectOfType<GameManager>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
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