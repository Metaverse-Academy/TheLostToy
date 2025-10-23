using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AutoDoorController : MonoBehaviour
{
    // ... (كل المتغيرات تبقى كما هي)
    [Header("Door Settings")]
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openSpeed = 2.0f;

    [Header("Trigger Zone")]
    [SerializeField] private Vector3 triggerSize = new Vector3(3, 2, 3);

    [Header("Sound Settings")]
    public AudioClip openSound;

    [Header("Jumpscare Event")]
    public GameObject monsterToActivate;
    public AudioClip jumpscareSound;
    public Transform jumpscareSpawnPoint;

    // متغيرات داخلية
    private Quaternion initialRotation;
    private Quaternion openRotation;
    private bool isPlayerNear = false;
    private bool isMoving = false;
    private AudioSource audioSource;
    private bool jumpscareTriggered = false;

    void Awake()
    {
        initialRotation = transform.rotation;
        openRotation = initialRotation * Quaternion.Euler(0, openAngle, 0);

        BoxCollider trigger = gameObject.AddComponent<BoxCollider>();
        trigger.isTrigger = true;
        trigger.size = triggerSize;

        audioSource = GetComponent<AudioSource>();

        // --- تم حذف الكود الذي يخفي الكائن من هنا ---
        // if (monsterToActivate != null)
        // {
        //     monsterToActivate.SetActive(false); 
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (!isMoving)
            {
                if (openSound != null && !audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(openSound);
                }

                if (monsterToActivate != null && !jumpscareTriggered && jumpscareSpawnPoint != null)
                {
                    TriggerJumpscare();
                }

                StartCoroutine(RotateDoor(openRotation));
            }
        }
    }

    private void TriggerJumpscare()
    {
        jumpscareTriggered = true;

        // 1. انقل الكائن إلى نقطة الظهور
        monsterToActivate.transform.position = jumpscareSpawnPoint.position;
        monsterToActivate.transform.rotation = jumpscareSpawnPoint.rotation;

        // 2. قم بتشغيل صوت الصرخة
        if (jumpscareSound != null)
        {
            audioSource.PlayOneShot(jumpscareSound);
        }

        // 3. تأكد من أن الكائن نشط (في حال كان معطلاً بالخطأ)
        if (!monsterToActivate.activeSelf)
        {
            monsterToActivate.SetActive(true);
        }
    }
    
    // ... (باقي الكود يبقى كما هو)
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (!isMoving)
            {
                StartCoroutine(RotateDoor(initialRotation));
            }
        }
    }

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
