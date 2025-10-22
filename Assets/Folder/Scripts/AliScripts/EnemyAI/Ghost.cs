using UnityEngine;

public class Ghost : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Entered Ghost Area");
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Debug.Log("Player Hit by Ghost!");
                playerHealth.TakeDamage(50);
            }
        }
    }
}
