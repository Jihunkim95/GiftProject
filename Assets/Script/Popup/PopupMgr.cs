
using UnityEngine;

public class PopupMgr : MonoBehaviour
{
    public static PopupMgr Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowPopup(GameObject popup)
    {
    if (popup == null)
    {
        Debug.LogError("popup is null.");
        return;
    }
        popup.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        FocusUnityWebGL(); // 포커스를 Unity WebGL로 설정하는 추가 메서드 호출

    }

    public void HidePopup(GameObject popup)
    {
        popup.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        FocusUnityWebGL(); // 포커스를 Unity WebGL로 설정하는 추가 메서드 호출

    }

    private void FocusUnityWebGL()
{
#if UNITY_WEBGL && !UNITY_EDITOR
    Application.ExternalEval("document.getElementById('unity-canvas').focus();");
#endif
}

}
