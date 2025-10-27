using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Cinemachine;

public class GameTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float timeDuration = 60f;

    [Header("UI Elements")]
    public TextMeshProUGUI timerText;
    public Image timerDial; // <-- هذه هي الخانة التي ستظهر

    [Header("Color Settings")]
    public Color firstPeriodColor = Color.white;
    public Color secondPeriodColor = new Color(1f, 0.5f, 0f, 1f);
    public Color lastPeriodColor = Color.red;

    [Header("Screen Shake")]
    public CinemachineImpulseSource impulseSource;

    private float remainingTime;
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
        if (timerText != null)
        {
            float minutes = Mathf.FloorToInt(remainingTime / 60);
            float seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        if (timerDial != null)
        {
            timerDial.fillAmount = remainingTime / timeDuration;
        }
        
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
