using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOnWalk : MonoBehaviour
{
    [SerializeField] private ParticleSystem walkParticles; // κ±·λ μ μ¬ ν°μ€
    [SerializeField] private ParticleSystem fastParticles; // λΉ λ₯΄κ²μ§μΌ μ¬ ν°μ€
    //[SerializeField] private ParticleSystem jumpParticles; // νμ¬ ν°μ€

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
        // μΊλ¦­°κ μ Ώμ κ³  μ§μ΄μ€μΈμ§ μΈ
        bool grounded = characterController.IsGrounded();
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        bool moving = (horizontalInput != 0 || verticalInput != 0);

        // μ Ώμ κ³  μ§μ΄μ€μΌ ν°¬μ
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
        // μΊλ¦­°κ μ Ώμ μκ³ ν μ€μ΄ λΌλ©΄ ν ν°¬μ
        if (!grounded && !isJumping)
        {
           // jumpParticles.Play();
            isJumping = true;
        }

        // μΊλ¦­°κ μ Ώμκ³ ν νΌλ©΄ ν ν°μ€μ
        if (grounded && isJumping)
        {
           // jumpParticles.Stop();
            isJumping = false;
        }
    }

}


