using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;

    [Header("UI & Sound")]
    public Slider healthSlider;
    public HeartbeatSound heartbeatSound;
    public AudioClip damageSound;      // <-- إضافة جديدة: خانة لملف صوت الضرر
    private AudioSource audioSource;   // <-- إضافة جديدة: متغير لتخزين مصدر الصوت

    private float currentHealth;
    private GameManager gameManager;

    void Start()
    {
        currentHealth = maxHealth;
        gameManager = FindFirstObjectByType<GameManager>();
        UpdateHealthUI();

        // --- إعداد مصدر الصوت --- // <-- إضافة جديدة
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) { audioSource = gameObject.AddComponent<AudioSource>(); }
        // -------------------------
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // --- تشغيل صوت الضرر --- // <-- إضافة جديدة
        if (damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
        // -------------------------

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        UpdateHealthUI();

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

    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
        Debug.Log("Player Died");

        if (gameManager != null)
        {
            gameManager.GameOver();
        }
    }
}
