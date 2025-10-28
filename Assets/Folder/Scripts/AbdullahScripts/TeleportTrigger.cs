using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    private static ToySpawner toySpawner; // متغير ثابت للوصول السريع لمدير الترسبن

    void Start()
    {
        // ابحث عن مدير الترسبن مرة واحدة فقط
        if (toySpawner == null)
        {
            toySpawner = FindFirstObjectByType<ToySpawner>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // تحقق إذا كان اللاعب هو من دخل
        if (other.CompareTag("Player"))
        {
            if (toySpawner != null)
            {
                // استدعِ الدالة العامة في مدير الترسبن
                toySpawner.TriggerTeleport();
            }

            // عطل هذا المحفز لمنع تفعيله مرة أخرى
            gameObject.SetActive(false);
        }
    }
}
