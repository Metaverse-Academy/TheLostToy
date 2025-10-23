using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))] // <-- إضافة جديدة (لضمان وجود مكون الصوت)
public class AutoDoorController : MonoBehaviour
{
    [Header("Door Settings")]
    [Tooltip("زاوية الفتح بالدرجات. 90 درجة تعني فتح كامل للخارج.")]
    [SerializeField] private float openAngle = 90f;

    [Tooltip("سرعة فتح وإغلاق الباب. قيمة أعلى تعني حركة أسرع.")]
    [SerializeField] private float openSpeed = 2.0f;

    [Header("Trigger Zone")]
    [Tooltip("حجم المنطقة التي ستفعل الباب. يمكنك رؤيتها في المشهد كصندوق أخضر.")]
    [SerializeField] private Vector3 triggerSize = new Vector3(3, 2, 3);

    [Header("Sound Settings")] // <-- إضافة جديدة
    [Tooltip("المقطع الصوتي الذي سيعمل عند فتح الباب.")] // <-- إضافة جديدة
    public AudioClip openSound; // <-- إضافة جديدة

    // متغيرات داخلية لتتبع حالة الباب
    private Quaternion initialRotation;
    private Quaternion openRotation;
    private bool isPlayerNear = false;
    private bool isMoving = false;
    private AudioSource audioSource; // <-- إضافة جديدة

    void Awake()
    {
        // احفظ دوران الباب الأصلي (وهو مغلق)
        initialRotation = transform.rotation;
        // احسب دوران الباب عندما يكون مفتوحًا
        openRotation = initialRotation * Quaternion.Euler(0, openAngle, 0);

        // --- إنشاء منطقة التفعيل (Trigger) تلقائيًا ---
        BoxCollider trigger = gameObject.AddComponent<BoxCollider>();
        trigger.isTrigger = true;
        trigger.size = triggerSize;

        // --- الحصول على مكون الصوت ---
        audioSource = GetComponent<AudioSource>(); // <-- إضافة جديدة
    }

    // هذه الدالة تعمل عندما يدخل كائن ما إلى منطقة التفعيل
    private void OnTriggerEnter(Collider other)
    {
        // تحقق مما إذا كان الكائن الذي دخل هو اللاعب
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            // إذا لم يكن الباب يتحرك بالفعل، ابدأ في فتحه
            if (!isMoving)
            {
                // --- تشغيل الصوت ---
                if (openSound != null && !audioSource.isPlaying) // <-- إضافة جديدة
                {
                    audioSource.PlayOneShot(openSound); // <-- إضافة جديدة
                }
                StartCoroutine(RotateDoor(openRotation));
            }
        }
    }

    // هذه الدالة تعمل عندما يخرج كائن ما من منطقة التفعيل
    private void OnTriggerExit(Collider other)
    {
        // (الكود هنا يبقى كما هو بدون تغيير)
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (!isMoving)
            {
                StartCoroutine(RotateDoor(initialRotation));
            }
        }
    }

    // (باقي الكود يبقى كما هو بدون تغيير)
    private IEnumerator RotateDoor(Quaternion targetRotation)
    {
        isMoving = true;
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
            yield return null;
        }
        transform.rotation = targetRotation;
        isMoving = false;
        if (!isPlayerNear && transform.rotation != initialRotation)
        {
            StartCoroutine(RotateDoor(initialRotation));
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, triggerSize);
    }
}
