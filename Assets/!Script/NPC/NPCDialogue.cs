using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public Timer timer;
    public GameObject timerUI; // timer UI 패널
    public GameObject dialogueUI; // 대화 UI 패널
    public Button optionABtn; // A 버튼
    public Button optionBBtn; // B 버튼
    public Button optionCBtn; // C 버튼
    public GameObject pressFTxt; // 'F' 키 안내 텍스트

    private bool isPlayerInRange = false; // 플레이어가 상호작용 범위 내에 있는지 여부
    private int selectedOption = 0; // 현재 선택된 옵션 (0 = A, 1 = B)

    public static bool isDialogueActive = false; // 대화 UI가 활성화되어 있는지 여부

    void Start()
    {
        dialogueUI.SetActive(false); // 대화 UI를 비활성화한 상태로 시작
        timerUI.SetActive(false);
        pressFTxt.SetActive(false);

        optionABtn.onClick.AddListener(() => SelectOption(0)); // A 버튼 클릭 이벤트 리스너 추가
        optionBBtn.onClick.AddListener(() => SelectOption(1)); // B 버튼 클릭 이벤트 리스너 추가
        optionCBtn.onClick.AddListener(() => SelectOption(2)); // C 버튼 클릭 이벤트 리스너 추가
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F) && !isDialogueActive)
        {
            ToggleDialogue();
        }

        if (dialogueUI.activeSelf)
        {
            HandleSelectionInput();
        }
    }

    private void ToggleDialogue()
    {
        dialogueUI.SetActive(!dialogueUI.activeSelf); // 대화 UI 활성화/비활성화 전환
        isDialogueActive = dialogueUI.activeSelf; // 대화 UI가 활성화된 상태를 업데이트

        if (dialogueUI.activeSelf)
        {
            UpdateOptionHighlight(); // 선택된 옵션 강조 표시 업데이트
        }
    }

    private void HandleSelectionInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedOption = Mathf.Max(0, selectedOption - 1); // 선택지 위로 이동
            UpdateOptionHighlight();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedOption = Mathf.Min(2, selectedOption + 1); // 선택지 아래로 이동
            UpdateOptionHighlight();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            ConfirmSelection(); // 선택지 확정
        }
    }

    private void UpdateOptionHighlight()
    {
        pressFTxt.SetActive(false);

        // 선택된 옵션 강조 표시
        ColorBlock colors = optionABtn.colors;
        colors.normalColor = selectedOption == 0 ? Color.yellow : Color.white;
        optionABtn.colors = colors;

        colors = optionBBtn.colors;
        colors.normalColor = selectedOption == 1 ? Color.yellow : Color.white;
        optionBBtn.colors = colors;

        colors = optionCBtn.colors;
        colors.normalColor = selectedOption == 2 ? Color.yellow : Color.white;
        optionCBtn.colors = colors;
    }

    private void ConfirmSelection()
    {
        if (selectedOption == 0)
        {
            Debug.Log("A 선택됨");
            timerUI.SetActive(true);
            timer.StartTimer();
        }
        else if (selectedOption == 1)
        {
            Debug.Log("B 선택됨");
            // 초기화
            timer.ResetTimer();
        }
        else if (selectedOption == 2)
        {
            Debug.Log("C 선택됨");
            // C 옵션 선택 로직 구현
        }

        dialogueUI.SetActive(false); // 대화 UI 숨김
        isDialogueActive = false; // 대화 UI 비활성화 상태로 설정
    }

    private void SelectOption(int option)
    {
        selectedOption = option;
        ConfirmSelection();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pressFTxt.SetActive(true);
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            pressFTxt.SetActive(false);
            dialogueUI.SetActive(false); // 플레이어가 떠나면 대화 UI 숨김
            isDialogueActive = false; // 대화 UI 비활성화 상태로 설정
        }
    }
}
