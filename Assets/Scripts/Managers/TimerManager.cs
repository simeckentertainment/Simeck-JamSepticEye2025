using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class TimerManager : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float startTime = 60f;
    
    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI timerText;
    // Uncomment below if using legacy Text instead of TextMeshPro
    // [SerializeField] private Text timerText;
    
    private float currentTime;
    private bool isRunning = false;
    
    void Start()
    {
        currentTime = startTime;
        UpdateTimerDisplay();
        StartTimer(); // Auto-start the timer
    }
    
    void Update()
    {
        if (isRunning && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            
            if (currentTime <= 0)
            {
                currentTime = 0;
                isRunning = false;
                OnTimerComplete();
            }
            
            UpdateTimerDisplay();
        }
    }
    
    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
    
    private void OnTimerComplete()
    {
        Debug.Log("Timer Complete!");
        SceneManager.LoadScene("LoseScene");
    }
    
    // Public methods to control the timer
    public void StartTimer()
    {
        isRunning = true;
    }
    
    public void PauseTimer()
    {
        isRunning = false;
    }
    
    public void ResetTimer()
    {
        currentTime = startTime;
        isRunning = false;
        UpdateTimerDisplay();
    }
    
    public void AddTime(float seconds)
    {
        currentTime += seconds;
        UpdateTimerDisplay();
    }
    
    public float GetCurrentTime()
    {
        return currentTime;
    }
}