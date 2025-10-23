using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DoorOpenSound : MonoBehaviour
{
    public AudioClip openDoorSound;   
    public bool canPlay = true;      

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    
    public void PlayOpenSound()
    {
        if (canPlay && openDoorSound)
        {
            audioSource.PlayOneShot(openDoorSound);
            canPlay = false; 
        }
    }
}
