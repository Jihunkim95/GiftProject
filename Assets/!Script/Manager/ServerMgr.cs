using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ServerMgr : MonoBehaviour
{
    public static ServerMgr Instance;

    private void Awake()
    {
        // Singleton 패턴을 사용하여 인스턴스를 전역으로 유지
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 변경되어도 파괴되지 않음
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 데이터를 서버에 전송하는 메서드
    public void SendDataToServer(string playerName, float time)
    {
        if (time != 0f){
            StartCoroutine(SendDataCoroutine(playerName, time));
        } else {
            Debug.Log("time data is zero");
        }
    }

    private IEnumerator SendDataCoroutine(string playerName, float time)
    {
        string url = "https://api.cloudengineering.store/api/save-time"; // 서버의 API 엔드포인트

        // JSON 형식으로 데이터를 생성
        string json = JsonUtility.ToJson(new TimeAttackData(playerName, time));

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // 서버로 요청을 보냅니다.  
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error sending data: " + request.error);
        }
        else
        {
            Debug.Log("Data successfully sent to server!");
        }
    }

    [System.Serializable]
    private class TimeAttackData
    {
        public string playerName;
        public float time;

        public TimeAttackData(string playerName, float time)
        {
            this.playerName = playerName;
            this.time = time;
        }
    }
}
