using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerFootsteps : MonoBehaviour
{
    public CharacterController controller; 
    public AudioClip footstepSound;
    public float stepDelay = 0.5f; // الفترة بين كل خطوة

    private AudioSource audioSource;
    private float stepTimer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (controller != null && controller.velocity.magnitude > 0.1f && controller.isGrounded)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepDelay)
            {
                audioSource.PlayOneShot(footstepSound);
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }
}
