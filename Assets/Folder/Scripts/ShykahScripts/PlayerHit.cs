using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public AudioClip hurtSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RandomObject"))
        {
            audioSource.PlayOneShot(hurtSound);
            Debug.Log("Player got hit!");
        }
    }
}
