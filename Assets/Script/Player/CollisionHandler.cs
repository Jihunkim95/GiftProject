using System.Collections.Generic;
using UnityEngine;

//
public class QuizCollisionEvent: ICollisionEventHandler
{
    private PopupQuiz popupQuiz;

    public QuizCollisionEvent(PopupQuiz popupQuiz)
    {
        this.popupQuiz = popupQuiz;
    }

    public void HandleCollision(GameObject collisionObj)
    {
        popupQuiz.ShowPopup(collisionObj); // 퀴즈 팝업 표시
        Debug.Log("QuizCollisionEvent");
    }

}

// 완료 충돌 이벤트
public class CompleteCollisionEvent: ICollisionEventHandler
{
    private PopupComplete popupComplete;

    public CompleteCollisionEvent(PopupComplete popupComplete)
    {
        this.popupComplete = popupComplete;
    }

    public void HandleCollision(GameObject collisionObj){
        //Collections 클래스에서 처리해서 주석
        // popupComplete.ShowPopup();
        Debug.Log("CompleteCollisionEvent");
    }
}

public class CollisionHandler : MonoBehaviour
{
    private Dictionary<string, ICollisionEventHandler> collisionEvents;
    private GameObject collidedObject; // 충돌한 오브젝트를 저장
    private bool isCollided = false; // 충돌 여부를 저장
    private float resetDistance = 3f; // 일정 거리 기준

    public PopupQuiz popupQuiz; // PopupQuiz 스크립트를 연결
    public PopupComplete popupComplete; // PopupComplete 스크립트를 연결


    private void Start()
    {
        collisionEvents = new Dictionary<string, ICollisionEventHandler>
        {
            { "TriggerObject", new QuizCollisionEvent(popupQuiz) },
            { "CompletionObject", new CompleteCollisionEvent(popupComplete) }

        };
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        HandleCollision(hit.gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        HandleCollision(other.gameObject);
    }
    private void HandleCollision(GameObject collisionObject)
    {
        if (!isCollided && collisionEvents.TryGetValue(collisionObject.tag, out ICollisionEventHandler handler))
        {
            handler.HandleCollision(collisionObject);
            collidedObject = collisionObject;
            isCollided = true;
        }
    }

    private void Update()
    {
        // 플레이어가 충돌한 오브젝트에서 일정 거리 이상 멀어지면
        if (isCollided && collidedObject != null)
        {
            float distance = Vector3.Distance(transform.position, collidedObject.transform.position);
            if (distance > resetDistance)
            {
                Collider cd = collidedObject.GetComponent<Collider>();
                if (cd != null)
                {
                    cd.enabled = true; // Collider를 다시 활성화
                }
                isCollided = false; // 충돌 상태 초기화
            }
        }
    }
}
