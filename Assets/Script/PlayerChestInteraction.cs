using UnityEngine;
using UnityEngine.UI;
using Michsky.MUIP;
using TMPro;

public class PlayerChestInteraction : MonoBehaviour
{
    public GameObject panel; // Panel UI
    public TextMeshProUGUI titleText;   // Title Text UI
    public TextMeshProUGUI contentText; // Content Text UI
    public ButtonManager closeButton; // Modern UI Pack Button
    public AudioSource audioSource; // 효과음을 재생할 AudioSource 컴포넌트

    private void Start()
    {
        panel.SetActive(false); // 게임 시작 시 Panel을 비활성화
        closeButton.onClick.AddListener(ClosePanel);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TeamChest1"))
        {
            Invoke("OpenChest1", 3.0f); // 3초 뒤에 OpenChest1 함수 호출

        }
        else if (other.CompareTag("TeamChest2"))
        {
            Invoke("OpenChest2", 3.0f); // 3초 뒤에 OpenChest1 함수 호출

        }
        else if (other.CompareTag("TeamChest3"))
        {
            Invoke("OpenChest3", 3.0f); // 3초 뒤에 OpenChest1 함수 호출

        }
        else if (other.CompareTag("SoloChest1"))
        {
            Invoke("SoloOpenChest1", 3.0f); 
        }
        else if (other.CompareTag("SoloChest2"))
        {
            Invoke("SoloOpenChest2", 3.0f); 

        }
        else if (other.CompareTag("SoloChest3"))
        {
            Invoke("SoloOpenChest3", 3.0f);

        }
        else if (other.CompareTag("SoloChest4"))
        {
            Invoke("SoloOpenChest4", 3.0f);

        }

        
    }

    void OpenChest1()
    {
        ShowPanel("OVDB", "김지용, 이승준 \n허원일, 손빈","5,000₩");

    }


    void OpenChest2()
    {
        ShowPanel("404", "하시온, 이유진 \n김선호, 정소은","5,000₩");

    }


    void OpenChest3()
    {
        ShowPanel("Spadework", "박종찬, 최건우 \n신수정, 김준도","5,000₩");
        
    }

    void SoloOpenChest4()
    {
        ShowPanel("Easy우상", "이지우","10,000₩");
        
    }

    void SoloOpenChest3()
    {
        ShowPanel("노력이 가상", "이훈석","10,000₩");
        
    }


    void SoloOpenChest2()
    {
        ShowPanel("안주면 속상", "박천균","10,000₩");
        
    }

    void SoloOpenChest1()
    {
        ShowPanel("기술이 명확상", "변성호","10,000₩");
        
    }
    void ShowPanel(string title, string content, string btnTxt)
    {
        //효과음 재생
        if (audioSource != null)
        {
            audioSource.Play();
        }

        titleText.text = title;
        contentText.text = content;
        closeButton.buttonText = btnTxt;
        panel.SetActive(true); // Panel을 활성화
        // Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void ClosePanel()
    {
        panel.SetActive(false); // Panel을 비활성화
        // Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
