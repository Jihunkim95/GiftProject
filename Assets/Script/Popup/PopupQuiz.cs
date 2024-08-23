using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro; // TextMeshPro를 사용하기 위해 추가
using System.Collections.Generic;

public class PopupQuiz : MonoBehaviour
{
    public GameObject popupPanel; // 팝업 패널을 연결
    public Button OButton;  // 확인 버튼을 연결
    public Button XButton;   // 취소 버튼을 연결
    public TextMeshProUGUI questionText;     // 퀴즈 질문을 표시할 텍스트

    private GameObject collidedObject; // 충돌한 오브젝트를 저장
    private QuizData currentQuiz; // 현재 퀴즈 데이터를 저장

    private List<QuizData> quizList; // 퀴즈 목록을 저장할 리스트

    private void Start()
    {
        popupPanel.SetActive(false); // 초기에는 팝업을 비활성화
        OButton.onClick.AddListener(OnOButtonClicked);
        XButton.onClick.AddListener(OnXButtonClicked);

        // 퀴즈 목록 초기화
        InitializeQuizzes();
    }


    // 퀴즈 목록 초기화 함수
    private void InitializeQuizzes()
    {
        quizList = new List<QuizData>
        {
            new QuizData("클라우드 컴퓨팅의 세 가지 주요 서비스 모델은 Iaas, Pass, Saas이다.", true), // "O"가 정답인 퀴즈
            new QuizData("클라우드 컴퓨팅의 주요 특징으로는 고정된 가격 모델이 있다.", false), // "X"가 정답인 퀴즈
            new QuizData("SSH는 네트워크상의 다른 컴퓨터에 로그인하여 명령을 실행하고 정보를 보고 받을수 있도록 해 주는 통신 프로토콜이다.", true), // "O"가 정답인 퀴즈
            new QuizData("쿠버네티스에서 기본 배포 단위는 노드이다.", false), // "X"가 정답인 퀴즈
            new QuizData("OSI 7레이어의 3계층 전송 단위는 패킷 또는 IP datagram이다.", true),
            new QuizData("NAT은 2계층 네트워크 장비로 동작한다.", false),
            new QuizData("리눅스 운영체제에서 사용자의 명령 해석기 기능을 수행하는것은 터미널이다.", true),
            new QuizData("-rwxr-xr-x 2 user1 project 80 5월 7일 14:32 file1 은 어떤 사용자라도 실행 시킬 수 있다.", true),
            new QuizData("shell에서 ${변수}는 변수 대체시 사용한다.", true),
            new QuizData("shell에서 $(command)는 명령 대체 구문으로 사용한다.", true),
            new QuizData("공유 디렉토리에 적용된 특수 퍼미션 setuid는 주로 실행파일에 적용되며 EUID가 바뀌는 특수퍼미션이다.", true),
            new QuizData("#crontab -l \n 30,40 1-2 * * 0 who –u >> /var/tmp/who.list는 일요일 1시 30분, 2시 40분에만 처리된다.", false),
            new QuizData("Ingress 리소스 확인 명령어는 kubectl get ingress이다.", true),
            new QuizData("YAML파일명이 ingress.yaml일때 Ingress 리소스 생성 명령어는 kubectl apply -f ingress.yaml이다.", true),
            new QuizData("kubectl get svc -n ingress-nginx은 리소스 생성 명령어다.", false),
            new QuizData("패키지를 설치 후 설치 확인 명령어는 rpm -qa|grep 패키지_이름.", true),
            new QuizData("리눅스 명령어 중 표준 입력은 > 이며, 출력은 < 이다.", false),
            new QuizData("리눅스 명령어 중 background 처리시 사용하는 특수 문자는 % 이다.", true),
            new QuizData("하드디스크 일부를 메모리처럼 사용하는 기술로 Swap이 사용된다.", true),
            new QuizData("nslookup은 특정 호스트의 IP주소를 찾기위해 사용된다.", true),

        };
    }


    public void ShowPopup(GameObject collidedObject)
    {
        this.collidedObject = collidedObject; // 충돌한 오브젝트 저장
       // 무작위로 퀴즈 선택
        currentQuiz = quizList[Random.Range(0, quizList.Count)];    
        // 선택된 퀴즈 질문을 표시
        questionText.text = currentQuiz.Question;
        // panel 활성화
        PopupMgr.Instance.ShowPopup(popupPanel);
    }

    private void OnOButtonClicked()
    {
        CheckAnswer(true);
    }

    private void OnXButtonClicked()
    {
        CheckAnswer(false);
    }

    private void CheckAnswer(bool selected)
    {
        // PopupManager 팝업 숨기기
        PopupMgr.Instance.HidePopup(popupPanel);
  
        // 충돌한 오브젝트의 물리적 속성을 비활성화
        bool isCorrect = (selected == currentQuiz.IsCorrectAnswer);
        if (!isCorrect && collidedObject != null)
        {
            Collider cd = collidedObject.GetComponent<Collider>();

            if (cd != null)
            {
                cd.enabled = false; // 오브젝트가 물리적 영향을 받지 않도록 설정
            }
        }
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

public class QuizData
{
    public string Question { get; set; }
    public bool IsCorrectAnswer { get; set; }

    public QuizData(string question, bool isCorrectAnswer)
    {
        Question = question;
        IsCorrectAnswer = isCorrectAnswer;
    }
}