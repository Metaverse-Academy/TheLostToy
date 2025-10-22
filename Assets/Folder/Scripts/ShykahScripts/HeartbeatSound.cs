using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HeartbeatSound : MonoBehaviour
{
    [Header("Settings")]
    public AudioClip heartbeatClip; // صوت دقات القلب
    public float minPitch = 0.8f;   // أبطأ دقة (عند صحة كاملة)
    public float maxPitch = 1.5f;   // أسرع دقة (عند صحة منخفضة)
    public float minVolume = 0.1f;
    public float maxVolume = 1.0f;

    [Header("Player Health (اختياري)")]
    [Range(0, 100)]
    public float playerHealth = 100f;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = heartbeatClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    void Update()
    {
        
        float healthPercent = Mathf.Clamp01(playerHealth / 100f);

        
        audioSource.pitch = Mathf.Lerp(maxPitch, minPitch, healthPercent);
        audioSource.volume = Mathf.Lerp(maxVolume, minVolume, healthPercent);
    }
}
