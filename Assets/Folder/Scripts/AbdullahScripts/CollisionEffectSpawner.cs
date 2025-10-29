using UnityEngine;

public class CollisionEffectSpawner : MonoBehaviour
{
    [Header("إعدادات التأثير")]
    [Tooltip("اسحب هنا الـ Prefab الخاص بتأثير الغبار")]
    public GameObject collisionEffectPrefab;

    [Tooltip("أقل سرعة مطلوبة للاصطدام لتشغيل التأثير. يمنع تشغيل التأثير عند اللمسات الخفيفة.")]
    public float minImpactVelocity = 1.5f;

    // هذه الدالة يتم استدعاؤها تلقائياً بواسطة محرك الفيزياء عند حدوث اصطدام
    private void OnCollisionEnter(Collision collision)
    {
        // collision.relativeVelocity.magnitude هي القوة النسبية للاصطدام
        if (collisionEffectPrefab != null && collision.relativeVelocity.magnitude > minImpactVelocity)
        {
            // --- تشغيل التأثير ---

            // 1. احصل على نقطة التلامس الأولى في الاصطدام
            ContactPoint contact = collision.contacts[0];
            Vector3 position = contact.point;
            Quaternion rotation = Quaternion.LookRotation(contact.normal); // اجعل التأثير يتجه للخارج من السطح

            // 2. أنشئ نسخة من الـ Prefab في مكان الاصطدام
            Instantiate(collisionEffectPrefab, position, rotation);

            Debug.Log($"اصطدام! سرعة: {collision.relativeVelocity.magnitude}. تشغيل تأثير الغبار.");
        }
    }
}
