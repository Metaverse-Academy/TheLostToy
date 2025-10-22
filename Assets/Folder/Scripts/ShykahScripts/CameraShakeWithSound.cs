using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CameraShakeWithSound : MonoBehaviour
{
    [Header("Shake Settings")]
    public float shakeDuration = 0.5f;   // كم ثانية يستمر الاهتزاز
    public float shakeAmount = 0.2f;     // قوة الاهتزاز
    public float shakeInterval = 60f;    // كل كم ثانية يحدث الاهتزاز (دقيقة = 60)

    [Header("Sound Settings")]
    public AudioClip rumbleSound;        // صوت الاهتزاز (اهتزاز السقف)
    public float soundVolume = 1.0f;     // مستوى الصوت

    private AudioSource audioSource;
    private float timer = 0f;
    private Vector3 originalPos;
    private bool isShaking = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        originalPos = transform.localPosition;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shakeInterval && !isShaking)
        {
            StartCoroutine(Shake());
            timer = 0f;
        }
    }

    private System.Collections.IEnumerator Shake()
    {
        isShaking = true;
        float elapsed = 0.0f;

        // تشغيل صوت الاهتزاز
        if (rumbleSound)
            audioSource.PlayOneShot(rumbleSound, soundVolume);

        while (elapsed < shakeDuration)
        {
            // موقع عشوائي خفيف حول الوضع الأصلي
            Vector3 randomPoint = originalPos + Random.insideUnitSphere * shakeAmount;
            transform.localPosition = randomPoint;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // إعادة الكاميرا لموقعها الطبيعي
        transform.localPosition = originalPos;
        isShaking = false;
    }
}
