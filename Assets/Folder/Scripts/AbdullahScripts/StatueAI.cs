using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class StatueAI : MonoBehaviour
{
    [Header("Core Settings")]
    [Tooltip("اسحب هنا كائن اللاعب (الذي له تاج Player)")]
    public Transform playerTarget;
    [Tooltip("اسحب هنا كاميرا اللاعب (الغالباً تكون Main Camera التي تتحكم بها Cinemachine)")]
    public Camera playerCamera; // <-- إضافة جديدة ومهمة

    [Header("AI Settings")]
    public float moveSpeed = 3.5f;
    [Tooltip("دقة مجال الرؤية. 0.8 يعني يجب أن يكون التمثال في منتصف الشاشة تقريباً.")]
    [Range(0f, 1f)]
    public float viewPrecision = 0.8f;

    [Header("Damage Settings")]
    public float damageDistance = 2.0f;
    public float damagePerSecond = 10f;

    [Header("Debug")]
    [Tooltip("ضع علامة هنا لترى رسائل Debug في الـ Console")]
    public bool enableDebugLogs = false;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

        // --- التحقق من وجود اللاعب والكاميرا ---
        if (playerTarget == null)
        {
            Debug.LogError("لم يتم تحديد اللاعب (Player Target) في الـ Inspector!", this);
            this.enabled = false;
            return;
        }
        if (playerCamera == null)
        {
            Debug.LogError("لم يتم تحديد كاميرا اللاعب (Player Camera) في الـ Inspector!", this);
            this.enabled = false;
            return;
        }
    }

    void Update()
    {
        if (IsVisibleToPlayer())
        {
            agent.isStopped = true;
            if (enableDebugLogs) Debug.Log(gameObject.name + ": Player is looking. STOPPING.");
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(playerTarget.position);
            if (enableDebugLogs) Debug.Log(gameObject.name + ": Player is NOT looking. MOVING.");
        }

        if (Vector3.Distance(transform.position, playerTarget.position) <= damageDistance)
        {
            DamagePlayer();
        }
    }

    private bool IsVisibleToPlayer()
    {
        Vector3 directionToStatue = (transform.position - playerCamera.transform.position).normalized;
        float dotProduct = Vector3.Dot(playerCamera.transform.forward, directionToStatue);

        if (dotProduct < viewPrecision)
        {
            if (enableDebugLogs) Debug.Log("Dot Product: " + dotProduct + " (Outside FoV)");
            return false;
        }

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, directionToStatue, out hit))
        {
            if (hit.transform.IsChildOf(this.transform) || hit.transform == this.transform)
            {
                if (enableDebugLogs) Debug.Log("Raycast hit the statue. IT IS VISIBLE.");
                return true;
            }
        }
        
        if (enableDebugLogs) Debug.Log("Raycast did not hit the statue (hit " + (hit.transform != null ? hit.transform.name : "nothing") + "). NOT VISIBLE.");
        return false;
    }

    private void DamagePlayer()
    {
        PlayerHealth playerHealth = playerTarget.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }
}
