using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class CeilingTimer : MonoBehaviour
{
    public float totalTime = 180f;
    public Volume volumeShake;
    public float effectDuration = 5f;
    public GameObject ceiling;
    private PlayerHealth playerHealth;

    private float timer;
    private ChromaticAberration chromatic;
    private LensDistortion distortion;

    void Start()
    {
        timer = totalTime;
        if (volumeShake.profile.TryGet(out chromatic) && volumeShake.profile.TryGet(out distortion))
        {
            chromatic.intensity.value = 0f;
            distortion.intensity.value = 0f;
        }
        StartCoroutine(Countdown());
    }
    IEnumerator Countdown()
    {
        while (timer > 0)
        {
            timer -= 1f;
            Debug.Log("Time Remaining: " + timer + " seconds");
            if (Mathf.Approximately(timer % 60f, 0f))
            {
                StartCoroutine(ShakeEffect());
            }
            yield return new WaitForSeconds(1f);
        }
        CollapseCeiling();
    }
    IEnumerator ShakeEffect()
    {
        float elapsed = 0f;
        while (elapsed < effectDuration)
        {
            if (chromatic != null)
                chromatic.intensity.value = Mathf.PingPong(Time.time * 5f, 1f);
            if (distortion != null)
                distortion.intensity.value = Mathf.PingPong(Time.time * 3f, 0.4f);

            elapsed += Time.deltaTime;
            yield return null;
        }
        if (chromatic != null)
            chromatic.intensity.value = 0f;
        if (distortion != null)
            distortion.intensity.value = 0f;
    }
    void CollapseCeiling()
    {
        Debug.Log("Ceiling Collapsed!");
        if (ceiling != null)
        {
            Rigidbody rb = ceiling.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
            CeilingCollisionDetector detector = ceiling.GetComponent<CeilingCollisionDetector>();
            if (detector == null)
            {
                detector = ceiling.AddComponent<CeilingCollisionDetector>();
            }
        }
    }
}
