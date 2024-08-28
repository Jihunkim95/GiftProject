
using UnityEngine;
using TMPro;

public class PlayerNameDisplay : MonoBehaviour
{
    public Transform player;  // 플레이어 Transform
    public Canvas worldSpaceCanvas;  // World Space Canvas
    public TextMeshProUGUI nameText;  // TMP 텍스트 컴포넌트
    private Vector3 offset = new Vector3(0, 1.6f, 0);  // PlayerArmature의 위에 텍스트를 고정하기 위한 오프셋 값 설정

    private void Start()
    {
        // PlayerPrefs에서 저장된 캐릭터 이름을 불러옵니다.
        if (PlayerPrefs.HasKey("CharacterName"))
        {
            string playerName = PlayerPrefs.GetString("CharacterName");

            if (nameText != null)
            {
                nameText.text = playerName; // 텍스트 컴포넌트에 이름을 표시합니다.
            }
            else
            {
                Debug.LogError("nameText is not assigned in the Inspector!");
            }

        }

    }

    private void LateUpdate()
    {
        if (player != null)
        {
            // Canvas를 PlayerArmature의 위치 위로 이동
            worldSpaceCanvas.transform.position = player.position + offset;
            // Canvas가 항상 카메라를 향하도록 회전
            worldSpaceCanvas.transform.LookAt(Camera.main.transform);
            worldSpaceCanvas.transform.Rotate(0, 180, 0); // 텍스트가 반대 방향을 보도록 180도 회전
        }
    }

    public void SetCharacterName(string name)
    {
        Debug.Log("SetCharacterName: "+ name);
        if (nameText != null)
        {
                    Debug.Log("nameText.text: "+ nameText.text);

            nameText.text = name;
        }
    }
}
