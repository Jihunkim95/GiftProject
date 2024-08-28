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
    public PlayerNameDisplay playerNameDisplay;
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
        Debug.Log("gd");
        if(p < characters.Length-1)
        {
            Debug.Log("gd");
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
        Debug.Log("Selected Character: " + characters[p].name);
        Debug.Log("characterNameText: " + characterNameText.text);

        // 추가 로직: 캐릭터를 게임에서 사용할 수 있도록 설정
        // 캐릭터 이름을 업데이트합니다.
        // 캐릭터 이름을 PlayerPrefs에 저장합니다.
        if(characterNameText != null)
        {
            PlayerPrefs.SetString("CharacterName", characterNameText.text);
            PlayerPrefs.Save();  // PlayerPrefs를 저장합니다.
        }

        // 씬 전환
        SceneManager.LoadScene("GiftScenes"); // "GameScene"을 원하는 씬 이름으로 변경하세요.
    }
        // 캐릭터 이름을 업데이트하는 메서드 추가
    public void UpdateCharacterName()
    {
        Debug.Log("playerNameDisplay:" + playerNameDisplay);
        if (characterNameText != null && playerNameDisplay != null)
        {
        Debug.Log("UpdateCharacterName 호출: " + characterNameText.text);

            playerNameDisplay.SetCharacterName(characterNameText.text);
        }
    }

}
