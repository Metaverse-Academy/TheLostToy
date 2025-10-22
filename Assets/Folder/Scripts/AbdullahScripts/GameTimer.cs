using UnityEngine;
using TMPro;
using Unity.Cinemachine; // لا تنس إضافة هذا السطر للوصول إلى Cinemachine

public class GameTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float timeDuration = 180f;

    [Header("UI Elements")]
    public TextMeshProUGUI timerText;

    [Header("Color Settings")]
    public Color thirdMinuteColor = Color.white;
    public Color secondMinuteColor = new Color(1f, 0f, 0f, 0.5f);
    public Color lastMinuteColor = Color.red;

    [Header("Screen Shake")]
    [Tooltip("اسحب هنا الكائن الذي يحتوي على Cinemachine Impulse Source")]
    public CinemachineImpulseSource impulseSource; // المرجع لمصدر الاهتزاز

    private float remainingTime;
    private bool isTimerRunning = false;

    // متغيرات لتتبع ما إذا كان الاهتزاز قد حدث بالفعل لكل دقيقة
    private bool shakeAtTwoMinutesDone = false;
    private bool shakeAtOneMinuteDone = false;

    void Start()
    {
        remainingTime = timeDuration;
        isTimerRunning = true;
        UpdateTimerColor();
    }

    void Update()
    {
        if (isTimerRunning && remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            
            // استدعاء دالة التحقق من الاهتزاز
            CheckForScreenShake();

            UpdateTimerDisplay(remainingTime);
            UpdateTimerColor();
        }
        else if (remainingTime <= 0)
        {
            remainingTime = 0;
            isTimerRunning = false;
            UpdateTimerDisplay(remainingTime);
            timerText.color = lastMinuteColor;
        }
    }

    private void CheckForScreenShake()
    {
        // التأكد من وجود مرجع لمصدر الاهتزاز لتجنب الأخطاء
        if (impulseSource == null) return;

        // عند الوصول إلى الدقيقة الثانية (أقل من أو يساوي 120 ثانية)
        if (remainingTime <= 120f && !shakeAtTwoMinutesDone)
        {
            impulseSource.GenerateImpulse();
            shakeAtTwoMinutesDone = true; // تم تنفيذ الاهتزاز، لا تكرره
            Debug.Log("Screen Shake at 2:00");
        }

        // عند الوصول إلى الدقيقة الأخيرة (أقل من أو يساوي 60 ثانية)
        if (remainingTime <= 60f && !shakeAtOneMinuteDone)
        {
            impulseSource.GenerateImpulse();
            shakeAtOneMinuteDone = true; // تم تنفيذ الاهتزاز، لا تكرره
            Debug.Log("Screen Shake at 1:00");
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
        if (remainingTime > 120) timerText.color = thirdMinuteColor;
        else if (remainingTime > 60) timerText.color = secondMinuteColor;
        else timerText.color = lastMinuteColor;
    }
}
