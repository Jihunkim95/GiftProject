using UnityEngine;

public class AnimatorChecker : MonoBehaviour
{
    public Animator animator;  // Animator 컴포넌트를 참조합니다.
    public RuntimeAnimatorController targetController; // 확인하려는 Animator Controller를 참조합니다.

    void Start()
    {
        // animator와 targetController가 모두 설정되어 있는지 확인
        if (animator != null && targetController != null)
        {
            // 현재 animator에 적용된 Animator Controller와 targetController가 같은지 확인
            if (animator.runtimeAnimatorController == targetController)
            {
                Debug.Log("The Animator has the correct controller applied.");
            }
            else
            {
                Debug.Log("The Animator does not have the correct controller applied.");
            }
        }
        else
        {
            Debug.LogWarning("Animator or targetController is not assigned.");
        }
    }
}
