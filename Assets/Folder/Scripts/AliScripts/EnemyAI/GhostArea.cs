using System.Collections;
using UnityEngine;

public class GhostArea : MonoBehaviour
{
    [Header("References")]
    private Transform player;
    private Rigidbody rb;

    [Header("Settings")]
    public float detectRange = 5f;
    public float floatHeight = 2f;
    public float floatDuration = 1f;
    public float launchForce = 15f;
    public float liftSpeed = 3f;

    private bool activated = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null)
            return;
        if (!activated && Vector3.Distance(player.position, transform.position) < detectRange)
        {
            StartCoroutine(PerformHaunt());
            activated = true;
        }
    }

    IEnumerator PerformHaunt()
    {
        Vector3 targetPos = transform.position + Vector3.up * floatHeight;
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;

        float elapsed = 0f;
        while (elapsed < 1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * liftSpeed);
            elapsed += Time.deltaTime;
            yield return null;
        }


        yield return new WaitForSeconds(floatDuration);

        rb.useGravity = true;
        Vector3 direction = (player.position - transform.position).normalized;
        rb.AddForce(direction * launchForce, ForceMode.Impulse);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}