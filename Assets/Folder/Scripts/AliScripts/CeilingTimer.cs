using UnityEngine;

public class CeilingTimer : MonoBehaviour
{
    public float totalTime = 180f;
    public VolumeShake volumeShake;
    public float effectDuration = 5f;

    private float timer;
    private ChromaticAberration chromatic;
    private LensDistortion distortion;

    void Start()
    {
        timer = totalTime;
        if(volumeShake.profile.TryGet(out chromaticAberration) && volumeShake.profile.TryGet(out distortion))
        {
            chromaticAberration.intensity.value = 0f;
            distortion.intensity.value = 0f;
        }

    }
    IEnumerator Countdown(){
        while (timer > 0)
        {
            timer -= 1f;
            if(Mathf.Approximately(timer % 60f, 0f))
            {
                StartCoroutine(ApplyVisualEffects());
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
        // Logic to collapse the ceiling
        Debug.Log("Ceiling Collapsed!");
    }
}
