using UnityEngine;
using UnityEngine.UI;
using Michsky.MUIP;
using TMPro;

public class ChSelectMgr : MonoBehaviour
{
    public GameObject[] characters;  // 캐릭터 오브젝트 배열
    public Button nextButton;
    public Button PrevButton;
    public TMP_InputField characterNameText;
    private int p = 0;

    void Start()
    {
        // 버튼에 이벤트 리스너를 추가합니다.
        nextButton.onClick.AddListener(Next);
        PrevButton.onClick.AddListener(Prev);

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
        // 추가 로직: 캐릭터를 게임에서 사용할 수 있도록 설정
    }
}
