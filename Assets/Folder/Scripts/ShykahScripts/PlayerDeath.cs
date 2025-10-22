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

       if (GetComponent<CharacterController>()) GetComponent<CharacterController>().enabled = false;
      
       var pm = GetComponent("PlayerMovement") as Behaviour;
       if (pm != null) pm.enabled = false;

        Debug.Log("Player Died!");
    }
}
