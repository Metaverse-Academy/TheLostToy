using UnityEngine;

public class CollisionEffectSpawner : MonoBehaviour
{
    [Header("Collision Effect Settings")]
    [Tooltip("Drag the dust effect prefab here")]
    public GameObject collisionEffectPrefab;

    [Tooltip("Minimum impact velocity required to trigger the effect. Prevents the effect from playing on light touches.")]
    public float minImpactVelocity = 1.5f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collisionEffectPrefab != null && collision.relativeVelocity.magnitude > minImpactVelocity)
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 position = contact.point;
            Quaternion rotation = Quaternion.LookRotation(contact.normal);

            Instantiate(collisionEffectPrefab, position, rotation);
        }
    }
}
