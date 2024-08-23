using System.Collections;
using UnityEngine;

public class Collections : MonoBehaviour
{
    public float rotationSpeed;

    public GameObject onCollectEffect;
    private AudioSource audioSource; // AudioSource 컴포넌트

    public PopupComplete popupComplete; // PopupComplete 스크립트를 연결

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Time.deltaTime);
        transform.Rotate(0,rotationSpeed * Time.deltaTime,0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(HandleCollection());
        }
    }

    private IEnumerator HandleCollection()
    {
        // 효과음 재생
        audioSource?.Play();

        // 수집 효과 실행
        if (onCollectEffect != null)
        {
            Instantiate(onCollectEffect, transform.position, transform.rotation);
        }

        // 오디오 클립 길이만큼 대기
        yield return new WaitForSeconds(audioSource?.clip.length ?? 0f);

        // CompletePopup 창 띄우기
        popupComplete.ShowPopup();

        // 오브젝트 삭제
        Destroy(gameObject);
    }
}