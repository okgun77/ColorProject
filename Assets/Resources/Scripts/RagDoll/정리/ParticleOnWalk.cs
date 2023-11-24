using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOnWalk : MonoBehaviour
{
    [SerializeField] private ParticleSystem walkParticles; // �ȴ� ?�� ?�� ?Ƽ?��
    [SerializeField] private ParticleSystem fastParticles; // ���������� ?�� ?Ƽ?��
    //[SerializeField] private ParticleSystem jumpParticles; // ?��?�� ?Ƽ?��

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
        // ĳ��?? ?�� ?�� ?�� ?���������� ?��
        bool grounded = characterController.IsGrounded();
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        bool moving = (horizontalInput != 0 || verticalInput != 0);

        // ?�� ?�� ?�� ?�������� ?Ƽ?��
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
        // ĳ��?? ?�� ?? ?��? ?�� ���� ?��?�� ?�� ?Ƽ?��
        if (!grounded && !isJumping)
        {
           // jumpParticles.Play();
            isJumping = true;
        }

        // ĳ��?? ?�� ?��? ?�� ?��?�� ?�� ?Ƽ��?
        if (grounded && isJumping)
        {
           // jumpParticles.Stop();
            isJumping = false;
        }
    }

}


