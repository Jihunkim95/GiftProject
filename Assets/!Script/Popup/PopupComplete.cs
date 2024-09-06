
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
        ServerMgr.Instance.SendDataToServer(playerName, elapsedTime);

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
}
