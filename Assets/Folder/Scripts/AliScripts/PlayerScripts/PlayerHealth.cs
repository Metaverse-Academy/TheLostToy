using UnityEngine;
using UnityEngine.UI; // <-- تمت إضافة هذا السطر للتحكم بالـ Slider

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;

    [Header("UI & Sound")]
    public Slider healthSlider; // <-- تمت إضافة هذه الخانة. اسحب شريط الصحة إلى هنا
    public HeartbeatSound heartbeatSound;

    private float currentHealth;
    private GameManager gameManager;

    void Start()
    {
        currentHealth = maxHealth;
        gameManager = FindFirstObjectByType<GameManager>(); // <-- تم التحديث للدالة الجديدة
        UpdateHealthUI(); // <-- تمت إضافة هذا السطر لتحديث الشريط في البداية
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // لا حاجة لـ (float) هنا لأن damage من نوع float أصلاً
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        UpdateHealthUI(); // <-- تمت إضافة هذا السطر لتحديث الشريط بعد تلقي الضرر

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

    // --- تمت إضافة هذه الدالة الجديدة ---
    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            // حساب النسبة وتحديث قيمة الـ Slider
            healthSlider.value = currentHealth / maxHealth;
        }
    }

    void Die()
    {
        // يفضل تعطيل الكائن بدلاً من تدميره لتجنب الأخطاء
        // Destroy(gameObject); 
        gameObject.SetActive(false);
        Debug.Log("Player Died");

        if (gameManager != null)
        {
            gameManager.GameOver();
        }
    }
}
