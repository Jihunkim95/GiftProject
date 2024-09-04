using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;  // 타이머를 표시할 UI 텍스트
    private float startTime;
    private bool isRunning = false;

    void Start()
    {
        // 타이머를 초기화하고 시작합니다.
        // startTime = 0f;
        // isRunning = true;
    }

    void Update()
    {
        if (isRunning)
        {
            // Time.unscaledDeltaTime을 사용하여 timeScale의 영향을 받지 않음
            startTime += Time.unscaledDeltaTime; 
            TimeSpan timeSpan = TimeSpan.FromSeconds(startTime);

            // 분, 초, 밀리초 형식으로 시간을 표시합니다.
            string timeText = string.Format("{0:D2}:{1:D2}.{2:D2}", 
                timeSpan.Minutes, 
                timeSpan.Seconds, 
                timeSpan.Milliseconds / 10); // 밀리초를 두 자리로 표시하기 위해 10으로 나눕니다.
            
            timerText.text = timeText;
        }
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        isRunning = false;
        startTime = 0f;
        timerText.text = "00:00.00";
    }

    public float GetElapsedTime()
    {
        return startTime;
    }
}
