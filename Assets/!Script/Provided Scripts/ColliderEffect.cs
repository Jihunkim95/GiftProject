using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEffect : MonoBehaviour
{
    private AudioSource audioSource; // AudioSource 컴포넌트

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    //충돌 메소드
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ball")){
            // 효과음 재생
        audioSource.Play();
        }

    }
}
