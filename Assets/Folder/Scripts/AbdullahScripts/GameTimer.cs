using UnityEngine;
using TMPro;
using UnityEngine.UI; // تأكد من وجود هذا السطر للتحكم بالـ UI
using Unity.Cinemachine; // تم تغييرها من Unity.Cinemachine لتكون متوافقة مع الإصدارات الأقدم إذا لزم الأمر

public class GameTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float timeDuration = 60f;

    [Header("UI Elements")]
    public TextMeshProUGUI timerText;
    public Image timerDial;
    public Slider timeSlider; // <-- تمت إضافة هذا السطر. اسحب شريط الوقت إلى هنا

    [Header("Color Settings")]
    public Color firstPeriodColor = Color.white;
    public Color secondPeriodColor = new Color(1f, 0.5f, 0f, 1f);
    public Color lastPeriodColor = Color.red;

    [Header("Screen Shake")]
    public CinemachineImpulseSource impulseSource;

    public float remainingTime;

    private bool isTimerRunning = false;
    private bool shakeAt40sDone = false;
    private bool shakeAt20sDone = false;

    void Start()
    {
        remainingTime = timeDuration;
        isTimerRunning = true;
        UpdateTimerUI();
    }

    void Update()
    {
        if (isTimerRunning && remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            CheckForScreenShake();
            UpdateTimerUI();
        }
        else if (isTimerRunning && remainingTime <= 0)
        {
            remainingTime = 0;
            isTimerRunning = false;
            UpdateTimerUI();
            Debug.Log("Time has run out!");
        }
    }

    private void UpdateTimerUI()
    {
        // حساب النسبة المئوية مرة واحدة لاستخدامها في كل العناصر
        float timePercentage = remainingTime / timeDuration;

        if (timerText != null)
        {
            float minutes = Mathf.FloorToInt(remainingTime / 60);
            float seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        if (timerDial != null)
        {
            timerDial.fillAmount = timePercentage;
        }

        // --- السطر الجديد الذي تمت إضافته ---
        if (timeSlider != null)
        {
            timeSlider.value = timePercentage; // تحديث قيمة شريط الوقت
        }
        // --- نهاية الإضافة ---
        
        Color currentColor;
        if (remainingTime > 40)
        {
            currentColor = firstPeriodColor;
        }
        else if (remainingTime > 20)
        {
            currentColor = secondPeriodColor;
        }
        else
        {
            currentColor = lastPeriodColor;
        }

        if (timerText != null) timerText.color = currentColor;
        if (timerDial != null) timerDial.color = currentColor;
        
        // (اختياري) يمكنك إضافة هذا السطر لتغيير لون شريط الوقت أيضاً
        // if (timeSlider != null) timeSlider.fillRect.GetComponent<Image>().color = currentColor;
    }

    private void CheckForScreenShake()
    {
        if (impulseSource == null) return;

        if (remainingTime <= 40f && !shakeAt40sDone)
        {
            impulseSource.GenerateImpulse();
            shakeAt40sDone = true;
        }

        if (remainingTime <= 20f && !shakeAt20sDone)
        {
            impulseSource.GenerateImpulse();
            shakeAt20sDone = true;
        }
    }
}
