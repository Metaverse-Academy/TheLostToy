using UnityEngine;
using TMPro;
using Unity.Cinemachine; 

public class GameTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    [Tooltip("مدة المؤقت بالثواني (الافتراضي 60 ثانية)")]
    public float timeDuration = 60f;

    [Header("UI Elements")]
    public TextMeshProUGUI timerText;
    public GameManager gameManager;


    [Header("Color Settings")]
    public Color firstPeriodColor = Color.white;
    public Color secondPeriodColor = Color.yellow;
    public Color lastPeriodColor = Color.red;

    [Header("Screen Shake")]
    [Tooltip("اسحب هنا الكائن الذي يحتوي على Cinemachine Impulse Source")]
    public CinemachineImpulseSource impulseSource; 

    public float remainingTime;
    private bool isTimerRunning = false;

    // --- متغيرات جديدة لتتبع الاهتزاز كل 20 ثانية ---
    private bool shakeAt40SecondsDone = false;
    private bool shakeAt20SecondsDone = false;

    void Start()
    {
        remainingTime = timeDuration;
        isTimerRunning = true;
        UpdateTimerColor();
    }

    void Update()
    {
        GameEnd();

        if (isTimerRunning && remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            
            // استدعاء دالة التحقق من الاهتزاز (الآن تعمل كل 20 ثانية)
            CheckForScreenShake(); 

            UpdateTimerDisplay(remainingTime);
            UpdateTimerColor();
        }
        else if (remainingTime <= 0)
        {
            remainingTime = 0;
            isTimerRunning = false;
            UpdateTimerDisplay(remainingTime);
            timerText.color = lastPeriodColor;
        }
    }

    // --- تم تعديل هذه الدالة بالكامل ---
    private void CheckForScreenShake()
    {
        // التأكد من وجود مرجع لمصدر الاهتزاز لتجنب الأخطاء
        if (impulseSource == null) return;

        // عند الوصول إلى 40 ثانية (أول 20 ثانية مرت)
        if (remainingTime <= 40f && !shakeAt40SecondsDone)
        {
            impulseSource.GenerateImpulse();
            shakeAt40SecondsDone = true; // تم تنفيذ الاهتزاز، لا تكرره
            Debug.Log("Screen Shake at 0:40");
        }

        // عند الوصول إلى 20 ثانية (ثاني 20 ثانية مرت)
        if (remainingTime <= 20f && !shakeAt20SecondsDone)
        {
            impulseSource.GenerateImpulse();
            shakeAt20SecondsDone = true; // تم تنفيذ الاهتزاز، لا تكرره
            Debug.Log("Screen Shake at 0:20");
        }
    }

    private void UpdateTimerDisplay(float timeToDisplay)
    {
        if (timeToDisplay < 0) timeToDisplay = 0;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void UpdateTimerColor()
    {
        if (remainingTime > 40)
        {
            timerText.color = firstPeriodColor;
        }
        else if (remainingTime > 20)
        {
            timerText.color = secondPeriodColor;
        }
        else
        {
            timerText.color = lastPeriodColor;
        }
    }
    void GameEnd()
    {
        if (remainingTime <= 0)
        {
            gameManager.GameOver();
        }
    }
}
