using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOnWalk : MonoBehaviour
{
    [SerializeField] private ParticleSystem walkParticles; // 걷는 ?안 ?올 ?티?스
    [SerializeField] private ParticleSystem fastParticles; // 빠르검직일 ?올 ?티?스
    //[SerializeField] private ParticleSystem jumpParticles; // ?프?올 ?티?스

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
        // 캐릭?? ?에 ?아 ?고 ?직이중인지 ?인
        bool grounded = characterController.IsGrounded();
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        bool moving = (horizontalInput != 0 || verticalInput != 0);

        // ?에 ?아 ?고 ?직이중일 ?티?생
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
        // 캐릭?? ?에 ?? ?았? ?프 중이 ?니?면 ?프 ?티?생
        if (!grounded && !isJumping)
        {
           // jumpParticles.Play();
            isJumping = true;
        }

        // 캐릭?? ?에 ?았? ?프 ?태?면 ?프 ?티중?
        if (grounded && isJumping)
        {
           // jumpParticles.Stop();
            isJumping = false;
        }
    }

}


