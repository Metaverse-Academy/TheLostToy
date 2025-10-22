using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HeartbeatSound : MonoBehaviour
{
    [Header("Settings")]
    public AudioClip heartbeatClip; // صوت دقات القلب
    public float minPitch = 0.8f;
    public float maxPitch = 1.5f;
    public float minVolume = 0.1f;
    public float maxVolume = 1.0f;

    [Header("Player Health (اختياري)")]
    [Range(0, 100)]


    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = heartbeatClip;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void UpdateHeartbeat(float healthPercentage)
    {
        float pitch = Mathf.Lerp(minPitch, maxPitch, 1 - (healthPercentage / 100f));
        float volume = Mathf.Lerp(minVolume, maxVolume, 1 - (healthPercentage / 100f));

        audioSource.pitch = pitch;
        audioSource.volume = volume;


    }
}
