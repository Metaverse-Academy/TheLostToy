using UnityEngine;
using System.Collections;

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

    // متغيرات داخلية لتتبع حالة الباب
    private Quaternion initialRotation;
    private Quaternion openRotation;
    private bool isPlayerNear = false;
    private bool isMoving = false;

    void Awake()
    {
        // احفظ دوران الباب الأصلي (وهو مغلق)
        initialRotation = transform.rotation;
        // احسب دوران الباب عندما يكون مفتوحًا
        openRotation = initialRotation * Quaternion.Euler(0, openAngle, 0);

        // --- إنشاء منطقة التفعيل (Trigger) تلقائيًا ---
        // هذا يغنيك عن إضافة Collider يدويًا
        BoxCollider trigger = gameObject.AddComponent<BoxCollider>();
        trigger.isTrigger = true; // اجعلها منطقة تفعيل، وليست حاجزًا صلبًا
        trigger.size = triggerSize;
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
                StartCoroutine(RotateDoor(openRotation));
            }
        }
    }

    // هذه الدالة تعمل عندما يخرج كائن ما من منطقة التفعيل
    private void OnTriggerExit(Collider other)
    {
        // تحقق مما إذا كان الكائن الذي خرج هو اللاعب
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            // إذا لم يكن الباب يتحرك بالفعل، ابدأ في إغلاقه
            if (!isMoving)
            {
                StartCoroutine(RotateDoor(initialRotation));
            }
        }
    }

    // هذه هي الدالة السحرية التي تحرك الباب بسلاسة (Coroutine)
    private IEnumerator RotateDoor(Quaternion targetRotation)
    {
        isMoving = true;

        // استمر في الدوران طالما أن الباب لم يصل إلى الزاوية المستهدفة
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            // استخدم دالة Lerp للتحرك بسلاسة من الدوران الحالي إلى الدوران المستهدف
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
            // انتظر الإطار التالي قبل مواصلة الحلقة
            yield return null;
        }

        // تأكد من أن الباب يصل إلى الزاوية الدقيقة في النهاية
        transform.rotation = targetRotation;
        isMoving = false;

        // تحقق إضافي: إذا خرج اللاعب أثناء فتح الباب، أغلقه فورًا
        if (!isPlayerNear && transform.rotation != initialRotation)
        {
            StartCoroutine(RotateDoor(initialRotation));
        }
    }

    // هذه الدالة ترسم الصندوق الأخضر في نافذة المشهد لتسهيل ضبط الحجم
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        // يجب أن نطبق نفس دوران ومقياس الكائن على الـ Gizmo
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, triggerSize);
    }
}
