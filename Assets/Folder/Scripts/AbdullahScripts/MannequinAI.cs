using UnityEngine;
using UnityEngine.AI;
public class MannequinAI : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Transform[] teleportPoints;
    private bool isVisible = false;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject[] points = GameObject.FindGameObjectsWithTag("TeleportPoint");
        teleportPoints = new Transform[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            teleportPoints[i] = points[i].transform;
        }
    }
    void OnBecameVisible()
    {
        isVisible = true;
    }
    void OnBecameInvisible()
    {
        isVisible = false;
        if (Vector3.Distance(transform.position, player.position) > 20f) return;
        TeleportToNewPosition();
    }
    void TeleportToNewPosition()
    {
        Transform bestPoint = null;
        float closestDistance = 15f;

        foreach (Transform point in teleportPoints)
        {
            float distance = Vector3.Distance(point.position, player.position);
            if (distance < closestDistance && distance > 3f)
            {
                bestPoint = point;
                closestDistance = distance;
            }
        }

        if (bestPoint != null)
        {
            agent.Warp(bestPoint.position);
            // Debug.Log("The mannequin has moved to: " + bestPoint.name);
        }
    }
}
