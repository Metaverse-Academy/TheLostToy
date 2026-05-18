using UnityEngine;
using System.Collections.Generic; // <-- مهم جداً لاستخدام القوائم (Lists)

public class ToySpawner : MonoBehaviour
{
    [Header("العناصر الأساسية")]
    public GameObject dollPrefab;
    public Transform[] spawnPoints;

    [Header("إعدادات الانتقال")]
    [Tooltip("عدد المرات التي ستنتقل فيها الدمية قبل أن تستقر")]
    public int maxTeleports = 3;

    // --- متغيرات جديدة لتتبع حالة اللعبة ---
    private GameObject currentDollInstance; // لتخزين الدمية الحالية وتدميرها لاحقاً
    private List<Transform> availableSpawnPoints; // قائمة بالأماكن المتاحة التي لم تستخدم بعد
    private int teleportsDone = 0; // عداد لمرات الانتقال

    void Start()
    {
        // تأكد من أن كل شيء جاهز
        if (spawnPoints.Length == 0 || dollPrefab == null)
        {
            Debug.LogError("السكربت يحتاج إلى Prefab ونقاط ترسبن ليعمل!");
            return;
        }

        // انسخ كل نقاط الترسبن إلى قائمة جديدة "متاحة" يمكننا حذف العناصر منها
        availableSpawnPoints = new List<Transform>(spawnPoints);

        // ابدأ اللعبة بوضع الدمية في مكان عشوائي أول
        TeleportDollToRandomPoint();
    }

    // --- هذه هي الدالة الجديدة والمهمة التي سيتم استدعاؤها من الخارج ---
    public void TriggerTeleport()
    {
        // إذا وصلنا للحد الأقصى من الانتقالات، لا تفعل شيئاً
        if (teleportsDone >= maxTeleports)
        {
            Debug.Log("الدمية استقرت في مكانها الأخير. لن تنتقل مجدداً.");
            return;
        }

        // قم بالانتقال إلى مكان عشوائي جديد
        TeleportDollToRandomPoint();
    }

    // --- قمنا بتطوير هذه الدالة لتصبح أكثر ذكاءً ---
    private void TeleportDollToRandomPoint()
    {
        // إذا لم تتبق أماكن متاحة، توقف
        if (availableSpawnPoints.Count == 0)
        {
            Debug.LogWarning("لا توجد نقاط متاحة متبقية!");
            return;
        }

        // إذا كانت هناك دمية قديمة، دمرها أولاً
        if (currentDollInstance != null)
        {
            Destroy(currentDollInstance);
        }

        // اختر نقطة عشوائية من القائمة "المتاحة" فقط
        int randomIndex = Random.Range(0, availableSpawnPoints.Count);
        Transform randomSpawnPoint = availableSpawnPoints[randomIndex];

        // أنشئ نسخة جديدة من الدمية واحفظها في المتغير
        currentDollInstance = Instantiate(dollPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);

        // الأهم: أزل هذه النقطة من القائمة المتاحة حتى لا يتم اختيارها مرة أخرى
        availableSpawnPoints.RemoveAt(randomIndex);

        // قم بزيادة عداد الانتقالات
        teleportsDone++;
    }
}
