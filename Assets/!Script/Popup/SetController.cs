using UnityEngine;
using UnityEngine.UI;
using Michsky.MUIP;

public class SetController : MonoBehaviour
{
    public GameObject lBCanvas;   // RankCanvas를 참조
    public ButtonManager rankButton;  // RankBtn을 참조
    public Button closeButton;   // RankCanvas를 참조

    private bool isRankCanvasActive = false; // RankCanvas 활성화 상태를 추적

    private void Start()
    {
        if (lBCanvas != null)
        {
            lBCanvas.SetActive(false); // RankCanvas 비활성화 상태로 시작
        }

        // RankBtn 클릭 이벤트에 ToggleRankCanvas 메서드를 연결합니다.
        if (rankButton != null && closeButton != null)
        {
            rankButton.onClick.AddListener(ToggleRankCanvas);
            closeButton.onClick.AddListener(ToggleRankCanvas);

        }

        // 게임 시작 시 커서를 숨김 상태로 설정합니다.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // ESC 키가 눌렸을 때 RankCanvas를 닫고 인게임으로 돌아갑니다.
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isRankCanvasActive)
            {
                CloseRankCanvas();
            }
            else
            {
                // ESC 키로 RankCanvas를 닫지 않은 경우는 마우스 커서를 활성화
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    // RankCanvas의 활성화 상태를 토글하는 메서드
    public void ToggleRankCanvas()
    {
        isRankCanvasActive = !isRankCanvasActive;
        lBCanvas.SetActive(isRankCanvasActive);

        // Canvas 활성화 상태에 따라 마우스 커서를 설정합니다.
        if (isRankCanvasActive)
        {
            Cursor.lockState = CursorLockMode.None; // 커서가 자유롭게 움직이도록 설정
            Cursor.visible = true;                  // 커서를 화면에 표시
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // 커서를 중앙에 고정
            Cursor.visible = false;                   // 커서를 화면에서 숨김
        }
    }

    // RankCanvas를 닫는 메서드
    private void CloseRankCanvas()
    {
        isRankCanvasActive = false;
        lBCanvas.SetActive(false);

        // 인게임 모드로 돌아가므로 마우스 커서를 숨깁니다.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
