
using UnityEngine;

// 충돌 이벤트 처리 인터페이스
public interface ICollisionEventHandler
{
    void HandleCollision(GameObject collisionObj);
}

public interface IRotator
{
    void Rotate();
}

public interface IAudioPlayer
{
    void PlayAudio();
}

public interface ICollectEffect
{
    void PlayEffect(Vector3 position, Quaternion rotation);
}
