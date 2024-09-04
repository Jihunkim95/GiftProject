
using UnityEngine;
using Michsky.MUIP;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using System.Collections;

public class PopupComplete : MonoBehaviour
{
    public GameObject popupPanel; // 팝업 패널을 연결
    public ButtonManager ResumeButton;  // 계속하기 버튼연결
    public Timer timer;
    public PlayerNameDisplay playerNameDisplay;


    private void Start()
    {
        popupPanel.SetActive(false); // 초기에는 팝업을 비활성화
        ResumeButton.onClick.AddListener(ResumeButtonClicked);

    }

    public void ShowPopup()
    {
        PopupMgr.Instance.ShowPopup(popupPanel);
        timer.StopTimer();

        // PlayerNameDisplay에서 플레이어 이름을 가져옵니다.
        string playerName = playerNameDisplay.GetPlayerName();
        float elapsedTime = timer.GetElapsedTime(); // 타이머로부터 경과 시간 가져오기

        // 서버에 데이터 전송
        StartCoroutine(SendDataToServer(playerName, elapsedTime));

    }

    private void ResumeButtonClicked()
    {
        PopupMgr.Instance.HidePopup(popupPanel);

    }
    private void Update()
    {
        if (popupPanel.activeSelf)
        {
            // Unscaled Time에 따라 UI 입력을 처리
            EventSystem.current.UpdateModules(); 
        }
    }
    
    // 서버통신

        private IEnumerator SendDataToServer(string playerName, float time)
    {
        string url = "http://localhost:3000/api/save-time"; // 서버의 API 엔드포인트

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
