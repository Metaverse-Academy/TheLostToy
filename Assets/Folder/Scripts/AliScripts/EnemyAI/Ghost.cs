using UnityEngine;

public class Ghost : MonoBehaviour
{
    public FlashlightHandler flashlightHandler;
    void Start()
    {
        flashlightHandler = FindObjectOfType<FlashlightHandler>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Debug.Log("Player Hit by Ghost!");
                playerHealth.TakeDamage(20);
                flashlightHandler.DropFlashlight();
            }
        }
    }
}