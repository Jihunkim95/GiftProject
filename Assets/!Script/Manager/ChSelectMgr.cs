using UnityEngine;
using UnityEngine.UI;
using Michsky.MUIP;
using TMPro;
using UnityEngine.SceneManagement;

public class ChSelectMgr : MonoBehaviour
{
    public static ChSelectMgr Instance; // 싱글톤 인스턴스

    public GameObject[] characters;  // 캐릭터 오브젝트 배열
    public Button nextButton;
    public Button PrevButton;
    public ButtonManager ConfirmButton;
    public TMP_InputField characterNameText;
    public TMP_Text StateTxt;

    private int p = 0;

    void Start()
    {
        // 버튼에 이벤트 리스너를 추가합니다.
        nextButton.onClick.AddListener(Next);
        PrevButton.onClick.AddListener(Prev);
        ConfirmButton.onClick.AddListener(ConfirmSelection);
        // 초기 캐릭터를 표시합니다.
        DisplayCharacter(p);
    }

    public void Next()
    {
        if(p < characters.Length-1)
        {
            characters[p].SetActive(false);
            p++;
            characters[p].SetActive(true);
        }
    }

    public void Prev()
    {
        if(p > 0)
        {
            characters[p].SetActive(false);
            p--;
            characters[p].SetActive(true);
        }
    }
    void DisplayCharacter(int index)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].SetActive(i == index);
        }

        // characterNameText.text = characters[index].name;
    }

    void ConfirmSelection()
    {
        if (characterNameText.text.Length > 5)
        {
            StateTxt.text = "5글자 이하로 입력해주세요.";
        }else{
            // 선택한 캐릭터 인덱스를 저장합니다.
            PlayerPrefs.SetInt("SelectedCharacterIndex", p);
            // 추가 로직: 캐릭터를 게임에서 사용할 수 있도록 설정
            // 캐릭터 이름을 업데이트합니다.
            if(characterNameText != null)
            {
                PlayerPrefs.SetString("CharacterName", characterNameText.text);
                PlayerPrefs.Save();  // PlayerPrefs를 저장합니다.
            }

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            // 씬 전환
            SceneManager.LoadScene("GiftScenes"); // "GameScene"을 원하는 씬 이름으로 변경하세요.
        }
 
    }
}
