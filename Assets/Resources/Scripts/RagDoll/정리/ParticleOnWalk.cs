using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOnWalk : MonoBehaviour
{
    [SerializeField] private ParticleSystem walkParticles; // ê±·ëŠ” ™ì•ˆ ˜ì˜¬ Œí‹°œìŠ¤
    [SerializeField] private ParticleSystem fastParticles; // ë¹ ë¥´ê²€ì§ì¼ ˜ì˜¬ Œí‹°œìŠ¤
    //[SerializeField] private ParticleSystem jumpParticles; // í”„˜ì˜¬ Œí‹°œìŠ¤

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
        // ìºë¦­°ê …ì— ¿ì•„ ˆê³  €ì§ì´ì¤‘ì¸ì§€ •ì¸
        bool grounded = characterController.IsGrounded();
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        bool moving = (horizontalInput != 0 || verticalInput != 0);

        // …ì— ¿ì•„ ˆê³  €ì§ì´ì¤‘ì¼ Œí‹°¬ìƒ
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
        // ìºë¦­°ê …ì— ¿ì Šì•˜ê³ í”„ ì¤‘ì´ „ë‹ˆ¼ë©´ í”„ Œí‹°¬ìƒ
        if (!grounded && !isJumping)
        {
           // jumpParticles.Play();
            isJumping = true;
        }

        // ìºë¦­°ê …ì— ¿ì•˜ê³ í”„ íƒœ¼ë©´ í”„ Œí‹°ì¤‘ì
        if (grounded && isJumping)
        {
           // jumpParticles.Stop();
            isJumping = false;
        }
    }

}


