
using UnityEngine;
using Michsky.MUIP;
using UnityEngine.EventSystems;

public class PopupComplete : MonoBehaviour
{
    public GameObject popupPanel; // 팝업 패널을 연결
    public ButtonManager ResumeButton;  // 계속하기 버튼연결

    private void Start()
    {
        popupPanel.SetActive(false); // 초기에는 팝업을 비활성화
        ResumeButton.onClick.AddListener(ResumeButtonClicked);

    }

    public void ShowPopup()
    {
        PopupMgr.Instance.ShowPopup(popupPanel);
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
