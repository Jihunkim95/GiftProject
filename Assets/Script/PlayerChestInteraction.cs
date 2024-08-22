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

    private Animator currentAnimator; //AniController

    private string currentChestTag;
    private bool isAnimating = false;

    private void Start()
    {
        panel.SetActive(false); // 게임 시작 시 Panel을 비활성화
        closeButton.onClick.AddListener(ClosePanel);

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if( !isAnimating && 
            other.CompareTag("TeamChest1")|| other.CompareTag("TeamChest2") ||
            other.CompareTag("TeamChest3")||other.CompareTag("SoloChest1") ||
            other.CompareTag("SoloChest2")||other.CompareTag("SoloChest3") ||
            other.CompareTag("SoloChest4"))
        {
            currentChestTag = other.tag;
            currentAnimator = other.GetComponent<Animator>();
            if(currentAnimator != null)
            {
                currentAnimator.SetTrigger("OpenChest");
                isAnimating = true; // 애니메이션 시작
            }
        }
  
        
    }

    private void Update()
    {
        if(isAnimating )
        {
                CheckAnimationState(); // 플레이어가 가까이 있을 때만 애니메이션 상태를 확인
        }
    }
    private void CheckAnimationState()
    {
        if (currentAnimator != null)
        {
            AnimatorStateInfo stateInfo = currentAnimator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Animated PBR Chest _Press") && stateInfo.normalizedTime >= 1.0f && !currentAnimator.IsInTransition(0))
            {
                ShowPanelBasedOnTag();
                isAnimating = false; // 애니메이션 종료
            }
        }
    }
    private void ShowPanelBasedOnTag()
    {
        switch (currentChestTag)
        {
            case "TeamChest1":
                OpenChest1();
                break;

        }
    }

    void OpenChest1()
    {
        ShowPanel("OVDB", "김지용, 이승준 \n허원일, 손빈","5,000₩");

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
