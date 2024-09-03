using UnityEngine;
using Cinemachine;

public class CinemachineZoom : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // Cinemachine Virtual Camera 참조
    public float zoomSpeed = 10f; // 줌 속도
    public float minFov = 15f; // 최소 FOV
    public float maxFov = 90f; // 최대 FOV

    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // 마우스 스크롤 입력에 따라 FOV 조정
        float currentFov = virtualCamera.m_Lens.FieldOfView;
        currentFov -= scrollInput * zoomSpeed;
        currentFov = Mathf.Clamp(currentFov, minFov, maxFov);

        virtualCamera.m_Lens.FieldOfView = currentFov;
    }
}
