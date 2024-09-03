using UnityEngine;

public class ChestController : MonoBehaviour
{
    public Animator chestAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chestAnimator.SetTrigger("OpenChest"); // 트리거를 설정하여 애니메이션 전환
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chestAnimator.ResetTrigger("OpenChest"); // 플레이어가 벗어나면 트리거 리셋
        }
    }
}
