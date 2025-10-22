using UnityEngine;

public class Ghost : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Debug.Log("Player Hit by Ghost!");
                playerHealth.TakeDamage(50);
            }
        }
    }
}
