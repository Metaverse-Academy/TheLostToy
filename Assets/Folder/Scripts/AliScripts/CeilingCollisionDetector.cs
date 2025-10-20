using UnityEngine;

public class CeilingCollisionDetector : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Ceiling collided with Player");
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Debug.Log("Player Hit by Ceiling!");
                playerHealth.TakeDamage(101);
            }
        }
    }
}