using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOnWalk : MonoBehaviour
{
    [SerializeField] private ParticleSystem walkParticles; // 걷는 동안 나올 파티클 시스템
    [SerializeField] private ParticleSystem fastParticles; // 빠르게 움직일 때 나올 파티클 시스템
    //[SerializeField] private ParticleSystem jumpParticles; // 점프할 때 나올 파티클 시스템

    private bool isJumping;
    private TPSPlayerController characterController;
    private bool isWalking;
    private bool isRunning;

    private void Start()
    {
        characterController = GetComponent<TPSPlayerController>();
        isWalking = false;
        isRunning = false;
    }

    private void Update()
    {
        // 캐릭터가 땅에 닿아 있고 움직이는 중인지 확인
        bool grounded = characterController.IsGrounded();
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        bool moving = (horizontalInput != 0 || verticalInput != 0);

        // 땅에 닿아 있고 움직이는 중일 때 파티클 재생
        if (grounded && moving)
        {
            if (!isWalking)
            {
                walkParticles.Play();
                isWalking = true;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (!isRunning)
                {
                    fastParticles.Play();
                    isRunning = true;
                }
            }
            else
            {
                if (isRunning)
                {
                    fastParticles.Stop();
                    isRunning = false;
                }
            }
        }
        else
        {
            if (isWalking)
            {
                walkParticles.Stop();
                isWalking = false;
            }

            if (isRunning)
            {
                fastParticles.Stop();
                isRunning = false;
            }
        }
        // 캐릭터가 땅에 닿지 않았고, 점프 중이 아니라면 점프 파티클 재생
        if (!grounded && !isJumping)
        {
           // jumpParticles.Play();
            isJumping = true;
        }

        // 캐릭터가 땅에 닿았고, 점프 상태라면 점프 파티클 중지
        if (grounded && isJumping)
        {
           // jumpParticles.Stop();
            isJumping = false;
        }
    }

}


