using UnityEngine;

public class PlayerEffectController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem; // 인스펙터에서 직접 할당할 파티클 시스템 배열
    
    // 특정 인덱스의 파티클 플레이
    public void PlayParticle(int index)
    {
        // Debug.Log("Particle System Play!");

        particleSystem.Play();
    }

    // 특정 인덱스의 파티클 정지
    public void StopParticle(int index)
    {
            particleSystem.Stop();
 
    }

    
    
}