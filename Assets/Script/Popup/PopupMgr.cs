
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
    }

    public void HidePopup(GameObject popup)
    {
        popup.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
