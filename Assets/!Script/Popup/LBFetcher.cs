using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class LBFetcher : MonoBehaviour
{
    public string apiUrl = "https://api.cloudengineering.store/api/get-times"; // 서버 API 엔드포인트
    public GameObject rowPrefab; // Row 프리팹 (Prefab 자체는 삭제되지 않음)
    public Transform tableTransform; // Table의 Transform (Content 또는 Entry)
    public Button refreshButton; // 버튼
    private float btnCooldown = 10f;
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
        // 버튼 클릭 시 순위표 갱신
        refreshButton.onClick.AddListener(OnUpdateLeaderboard);
        StartCoroutine(FetchLeaderboardData());
    }

    // 순위표 갱신 요청
    void OnUpdateLeaderboard()
    {
        StartCoroutine(BtnColldownRoutine());
        StartCoroutine(FetchLeaderboardData());
    }

    // 버튼 10초 비활성화 코루틴
    IEnumerator BtnColldownRoutine()
    {
        refreshButton.interactable = false; // 버튼 비활성화
        yield return new WaitForSeconds(btnCooldown); // 10초 대기
        refreshButton.interactable = true; // 버튼 활성화

    }

    // 서버에서 데이터를 가져오는 코루틴
    private IEnumerator FetchLeaderboardData()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        // Debug.Log("Sending Request to: " + apiUrl);
        request.timeout = 10;  // 타임아웃 시간을 10초로 설정
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.LogError("Error fetching leaderboard data: " + request.error);
            Debug.LogError("HTTP Response Code: " + request.responseCode);  // 응답 코드 추가 출력

        }
        else
        {
            Debug.Log("Request Successful!");

            // JSON 데이터를 파싱하여 리스트로 변환
            string jsonResult = request.downloadHandler.text;
            // Debug.Log("JSON Result: " + jsonResult);

            try
            {
                // JsonHelper를 사용하여 JSON 배열을 파싱
                PlayerData[] playerDataArray = JsonHelper.FromJson<PlayerData>(jsonResult);
                List<PlayerData> playerDataList = new List<PlayerData>(playerDataArray);

                // 순위표 갱신
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
        // 기존에 생성된 Row들을 모두 삭제 (rowPrefab은 건드리지 않음)
        foreach (Transform child in tableTransform)
        {
            // if (child.gameObject != rowPrefab) // rowPrefab은 삭제하지 않음
            // {
                Destroy(child.gameObject);
            // }
        }

        // 서버로부터 받은 데이터를 사용하여 순위표를 업데이트
        for (int i = 0; i < playerDataList.Count; i++)
        {
            PlayerData playerData = playerDataList[i];

            // Row 프리팹을 인스턴스화하고 Table의 자식으로 추가
            GameObject row = Instantiate(rowPrefab, tableTransform);

            // RankTxt, NameTxt, TimeTxt 텍스트를 업데이트
            var RankTxt = row.transform.Find("RankTxt").GetComponent<TextMeshProUGUI>();
            var NameTxt = row.transform.Find("NameTxt").GetComponent<TextMeshProUGUI>();
            var TimeTxt = row.transform.Find("TimeTxt").GetComponent<TextMeshProUGUI>();

            RankTxt.text = (i + 1).ToString();
            NameTxt.text = playerData.player_name;
            TimeTxt.text = playerData.time.ToString("F3");

            // UI 활성화
            RankTxt.enabled = true;
            NameTxt.enabled = true;
            TimeTxt.enabled = true;
        }
    }
}

// JSON 배열 파싱을 위한 헬퍼 클래스
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}
