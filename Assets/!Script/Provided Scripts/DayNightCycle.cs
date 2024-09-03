using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight;
    public float dayLengthInMinutes = 1.0f;
    
    private float dayLengthInSeconds;
    private float timeOfDay;

    void Start()
    {
        // 하루의 길이를 초 단위로 변환
        dayLengthInSeconds = dayLengthInMinutes * 60f;
    }

    void Update()
    {
        // 시간 업데이트
        timeOfDay += Time.deltaTime / dayLengthInSeconds;
        
        // 하루가 지나면 시간 초기화
        if (timeOfDay >= 1.0f)
        {
            timeOfDay = 0.0f;
        }

        // 태양의 회전 설정
        float sunRotation = timeOfDay * 360f;
        directionalLight.transform.rotation = Quaternion.Euler(new Vector3(sunRotation - 90f, 170f, 0f));
    }
}
