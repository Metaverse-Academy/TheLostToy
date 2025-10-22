using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public AudioClip deathSound;
    private AudioSource audioSource;
    private bool isDead = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        audioSource.Stop();
        audioSource.PlayOneShot(deathSound);


        // تعطيل الحركة والتحكم
        var cc = GetComponent<CharacterController>();
        if (cc) cc.enabled = false;
        // Use string-based lookup to avoid compile-time dependency on PlayerMovement
        var pm = GetComponent("PlayerMovement") as MonoBehaviour;
        if (pm != null) pm.enabled = false;


        Debug.Log("Player Died!");
    }
}
