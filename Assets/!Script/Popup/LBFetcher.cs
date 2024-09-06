using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class LBFetcher : MonoBehaviour
{
    public string apiUrl = "http://localhost:3000/api/get-times"; // 서버 API 엔드포인트
    public GameObject rowPrefab; // Row 프리팹
    public Transform tableTransform; // Table의 Transform (Content 또는 Entry)
    public Button refreshButton; // Row 프리팹

    // 플레이어 데이터 클래스
    [System.Serializable]
    public class PlayerData
    {
        public string player_name;  // 서버에서 보내는 필드 이름과 일치시킴
        public float time;
        public string timestamp;
    }

    [System.Serializable]
    public class PlayerDataList
    {
        public List<PlayerData> data;
    }

    private void Start()
    {
        refreshButton.onClick.AddListener(OnUpdateLeaderboard);
        StartCoroutine(FetchLeaderboardData());
    }
    void OnUpdateLeaderboard()
    {
        // 순위표 갱신 요청
        StartCoroutine(FetchLeaderboardData());
    }

    // 서버에서 데이터를 가져오는 코루틴
    private IEnumerator FetchLeaderboardData()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        Debug.Log("Sending Request to: " + apiUrl);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error fetching leaderboard data: " + request.error);
        }
        else
        {
            Debug.Log("Request Successful!");

            // JSON 데이터를 파싱하여 리스트로 변환
            string jsonResult = request.downloadHandler.text;
            Debug.Log("JSON Result: " + jsonResult);

            // 리스트로 변환 (필드 이름 변경 반영)
            try
            {
                PlayerData[] playerDataArray = JsonUtility.FromJson<PlayerData[]>(jsonResult);
                List<PlayerData> playerDataList = new List<PlayerData>(playerDataArray);
                Debug.Log("Player Data Parsed Successfully!");
                UpdateLeaderboard(playerDataList);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error parsing JSON data: " + ex.Message);
            }
        }
    }

    // 순위표를 업데이트하는 메서드
    private void UpdateLeaderboard(List<PlayerData> playerDataList)
    {
        // 기존에 생성된 Row들을 모두 삭제합니다.
        foreach (Transform child in tableTransform)
        {
            Destroy(child.gameObject);
        }

        // 서버로부터 받은 데이터를 사용하여 순위표를 업데이트합니다.
        for (int i = 0; i < playerDataList.Count; i++)
        {
            PlayerData playerData = playerDataList[i];

            // Row 프리팹을 인스턴스화하고 Table의 자식으로 추가합니다.
            GameObject row = Instantiate(rowPrefab, tableTransform);

            // RankTxt, NameTxt, TimeTxt 텍스트를 업데이트합니다.
            row.transform.Find("RankTxt").GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
            row.transform.Find("NameTxt").GetComponent<TextMeshProUGUI>().text = playerData.player_name;
            row.transform.Find("TimeTxt").GetComponent<TextMeshProUGUI>().text = playerData.time.ToString("F2");
        }
    }
}
