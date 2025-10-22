using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerBreathing : MonoBehaviour
{
    [Header("Breathing Clips")]
    public AudioClip breathingClip;   

    [Header("Settings")]
    public float normalPitch = 1f;    
        public float fastPitch = 1.5f;   
    public float changeInterval = 10f; 
    public float fastDuration = 3f;  

    private AudioSource audioSource;
    private float timer;
    private bool isFast = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = breathingClip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.pitch = normalPitch;
        audioSource.Play();

        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!isFast && timer >= changeInterval)
        {
          
            audioSource.pitch = fastPitch;
            isFast = true;
            timer = 0f;
            Invoke(nameof(ReturnToNormal), fastDuration);
        }
    }

    void ReturnToNormal()
    {
        audioSource.pitch = normalPitch;
        isFast = false;
    }
}
