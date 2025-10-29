using UnityEngine;
using UnityEngine.AI; // لا تنس إضافة هذا السطر

public class MannequinAI : MonoBehaviour
{
    public Transform player; // اسحب مجسم اللاعب إلى هذا المتغير في الـ Inspector
    private NavMeshAgent agent;
    private Transform[] teleportPoints;

    private bool isVisible = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // البحث عن كل نقاط الانتقال وتخزينها
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
        
        // لا يتحرك إذا كان بعيدًا جدًا عن اللاعب
        if (Vector3.Distance(transform.position, player.position) > 20f) return;

        TeleportToNewPosition();
    }

    void TeleportToNewPosition()
    {
        // ابحث عن أفضل نقطة انتقال (قريبة من اللاعب ولكن ليست قريبة جدًا)
        Transform bestPoint = null;
        float closestDistance = 15f; // أقصى مسافة للبحث

        foreach (Transform point in teleportPoints)
        {
            float distance = Vector3.Distance(point.position, player.position);
            if (distance < closestDistance && distance > 3f) // ابحث عن نقطة بين 3 و 15 متر
            {
                bestPoint = point;
                closestDistance = distance;
            }
        }

        if (bestPoint != null)
        {
            // انقل المانيكان إلى النقطة الجديدة
            agent.Warp(bestPoint.position); 
            Debug.Log("المانيكان انتقل إلى: " + bestPoint.name);
        }
    }
}
