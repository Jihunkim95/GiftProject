using Cinemachine;
using StarterAssets;
using UnityEngine;

public class GameSceneMgr : MonoBehaviour
{
    public GameObject[] characters; // 캐릭터 프리팹 배열
    public GameObject playerArmature; // Animator가 있는 빈 게임 오브젝트
    private Animator playerAnimator; // playerArmature의 Animator

    private void Start()
    {
        // PlayerPrefs에서 저장된 캐릭터 인덱스를 불러옵니다.
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacterIndex", 0);

        // playerArmature의 Animator 가져오기
        playerAnimator = playerArmature.GetComponent<Animator>();

        if (playerAnimator == null)
        {
            Debug.LogError("Animator is missing on playerArmature!");
            return;
        }

        // 기존 캐릭터 모델 모두 비활성화 제거, PlayerCameraRoot는 제외
        foreach (Transform child in playerArmature.transform)
        {
            if (child.gameObject.name != "PlayerCameraRoot")
            {
                Destroy(child.gameObject);
            }
        }

        // 선택된 캐릭터 프리팹을 인스턴스화하고 playerArmature의 하위로 설정합니다.
        GameObject selectedCharacter = Instantiate(characters[selectedCharacterIndex], playerArmature.transform);
        selectedCharacter.transform.localPosition = Vector3.zero;
        selectedCharacter.transform.localRotation = Quaternion.identity;

        // Animator 리바인드 (다시 연결)
        playerAnimator.Rebind(); // Animator 초기화
        playerAnimator.Update(0); // Animator가 상태를 업데이트할 수 있도록 0으로 업데이트합니다.

        Debug.Log("Selected character instantiated and Animator is set up.");
    }
}
