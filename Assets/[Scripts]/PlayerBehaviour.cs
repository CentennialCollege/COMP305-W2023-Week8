using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Movement Properties")] 
    public float horitzontalForce;
    public float maxSpeed;
    public float verticalForce;
    public float airFactor;
    public Transform groundPoint;
    public float groundRadius;
    public LayerMask groundLayerMask;
    public bool isGrounded;
    public PlayerAnimationState animationState;

    [Header("Screen Shake Properties")]
    public CinemachineVirtualCamera virtualCamera;
    public CinemachineBasicMultiChannelPerlin perlin;
    public float shakeIntensity;
    public float shakeDuration;
    public float shakeTimer;
    public bool isCameraShaking;

    [Header("PlayerBody Properties")]
    public Transform playerBody;
    public Rigidbody2D playerRigidBody2D;
    public Animator animator;

    private SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        soundManager = FindObjectOfType<SoundManager>();

        // camera shake
        isCameraShaking= false;
        shakeTimer = shakeDuration;
        virtualCamera = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
        perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void Update()
    {
        var y = Convert.ToInt32(Input.GetKeyDown(KeyCode.Space));
        Jump(y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, groundRadius, groundLayerMask);
        var x = Input.GetAxisRaw("Horizontal");

        Flip(x);
        Move(x);
        AirCheck();

        // camera Shake Control
        if(isCameraShaking)
        {
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0.0f) // timed out
            {
                perlin.m_AmplitudeGain = 0.0f;
                shakeTimer = shakeDuration;
                isCameraShaking = false;
            }
        }

    }

    private void Move(float x)
    {
        playerRigidBody2D.AddForce(Vector2.right * x * horitzontalForce * ((isGrounded) ? 1 : airFactor));

        playerRigidBody2D.velocity = new Vector2(Mathf.Clamp(playerRigidBody2D.velocity.x, -maxSpeed, maxSpeed), playerRigidBody2D.velocity.y);

        if (isGrounded)
        {
            if (x != 0.0f)
            {
                animationState = PlayerAnimationState.RUN;
                animator.SetInteger("AnimationState", (int)animationState);
            }
            else
            {
                animationState = PlayerAnimationState.IDLE;
                animator.SetInteger("AnimationState", (int)animationState);
            }
            
        }
    }

    private void Jump(int y)
    {
        if ((isGrounded) && (y > 0.0f))
        {
            playerRigidBody2D.AddForce(Vector2.up * verticalForce, ForceMode2D.Impulse);
            soundManager.PlaySoundFX(Channel.PLAYER_SOUND_FX, SoundFX.JUMP);
        }

    }

    private void AirCheck()
    {
        if (!isGrounded)
        {
            animationState = PlayerAnimationState.JUMP;
            animator.SetInteger("AnimationState", (int)animationState);
        }
    }

    private void Flip(float x)
    {
        if (x != 0)
        {
            playerBody.localScale = new Vector3((x > 0) ? 1 : -1, 1, 1);
        }
    }

    private void ShakeCamera()
    {
        perlin.m_AmplitudeGain = shakeIntensity;
        isCameraShaking= true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundPoint.position, groundRadius);
    }


    // TODO: Need to move this Script to PlayerBody
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            soundManager.PlaySoundFX(Channel.PICKUP, SoundFX.GEM);
            // gain points
        }

        if(other.gameObject.CompareTag("Hazard"))
        {
            ShakeCamera();
            soundManager.PlaySoundFX(Channel.PLAYER_HURT_FX, SoundFX.HURT);
            // lose health
        }
    }
}
