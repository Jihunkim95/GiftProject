using UnityEngine;
using UnityEngine.EventSystems;

public class ResetEventSystem : MonoBehaviour
{
    private EventSystem eventSystem;

    private void Start()
    {
        eventSystem = EventSystem.current;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            ResetEventSystemComponents();
        }
    }

    private void ResetEventSystemComponents()
    {
        if (eventSystem != null)
        {
            eventSystem.enabled = false;
            eventSystem.enabled = true; // EventSystem을 재설정하여 문제 해결 시도
        }
    }
}
