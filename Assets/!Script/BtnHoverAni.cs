using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // For EventTrigger
using DG.Tweening; // DOTween 네임스페이스

public class BtnHoverAni : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform buttonTransform; // 버튼의 RectTransform
    public float hoverScale = 1.2f; // Hover 시 크기 비율
    public float animationDuration = 0.2f; // 애니메이션 시간

    private Vector3 originalScale; // 버튼의 원래 크기

    private void Start()
    {
        // 버튼의 원래 크기 저장
        originalScale = buttonTransform.localScale;
    }

    // 마우스 Hover 시 호출
    public void OnPointerEnter(PointerEventData eventData)
    {
        // DOTween을 이용해 크기를 키우는 애니메이션 실행
        buttonTransform.DOScale(originalScale * hoverScale, animationDuration).SetEase(Ease.OutBack);
    }

    // 마우스가 버튼을 벗어났을 때 호출
    public void OnPointerExit(PointerEventData eventData)
    {
        // 원래 크기로 돌아가는 애니메이션 실행
        buttonTransform.DOScale(originalScale, animationDuration).SetEase(Ease.OutBack);
    }
}
