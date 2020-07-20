﻿using static System.Math;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    CharacterController CharacterController;
    public float speed = 12f, unCrouchSpeed = 12f, crouchSpeed = 6f, gravity = -20f, JumpHight = 3f, crouchHeight = 0.2f, unCrouchHeight = 0.5f, knockBackSpeed, knockBackSlowSpeed, slideSpeed, slideDuration, wallRunSpeed;
    [SerializeField]
    Transform groundcheck, cam;
    float groundistance = 0.2f;
    [SerializeField]
    LayerMask groundmask;
    [HideInInspector]
    public Vector3 velocity;
    [SerializeField]
    bool canWallJump, isSliding, isWallRunning;
    [HideInInspector]
    public bool isGrounded;
    public KeyCode crouch = KeyCode.LeftControl, sprint = KeyCode.LeftShift, interact = KeyCode.E;
    [HideInInspector]
    public Vector3 knockBackDir;
    float x, z, maxSlideDuration, slideX, slideZ, wallJumpSpeed;
    Vector3 moveRaw, move, oldPos, newPos, slideDir;
    [HideInInspector]
    public Vector3 wallRunDir, wallJumpMainDir;
    int tiltTime;
    public float climbSpeed = 0.5f, minFallDistance, fallDamage;
    [HideInInspector]
    public bool isClimbing;
    float verticalJumpDir;
    [HideInInspector]
    public bool touchingWall;
    float fallDuration;
    [SerializeField]
    Health health;
    float groundedTime;
    Vector3 wallRayDir;

    bool isFalling()
    {
        if (groundedTime > 0.4f || velocity.y > 0f) return false;
        if (!isGrounded && velocity.y < 0f) return true;
        return true;
    }

    void Start() => verticalJumpDir = (float)(Sqrt((double)(JumpHight * -2f * gravity)));

    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, .005f)) return;
        Jump();
        if (!(isWallRunning && Input.GetKey(sprint)))
        {
            Wasd();
            Sprint();
            Slide();
        }
        Cannon();
        WallRun();
        Climb();
        Vault();
        if (isFalling()) CheckForFallDamage();
        else fallDuration = 0f;
        if (isGrounded) groundedTime += Time.deltaTime;
        else groundedTime = 0f;
    }

    void Wasd()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        move = Vector3.ClampMagnitude(transform.right * x + transform.forward * z, 1f);
        if (move.magnitude != 0f) CharacterController.Move(move * speed * Time.deltaTime);
    }

    void Sprint()
    {
        if (Input.GetKey(sprint))
            if (move.magnitude != 0f) CharacterController.Move(move * speed / 3 * Time.deltaTime);
    }

    void Slide()
    {

        isSliding = isGrounded && Input.GetKey(crouch) && slideDuration > 0f;
        if (slideDuration < 0f) slideDuration = 0f;
        if (slideDuration > maxSlideDuration) slideDuration = maxSlideDuration;

        if (isSliding)
        {
            CharacterController.Move(slideDir * slideSpeed * Time.deltaTime);
            CharacterController.height = crouchHeight;
            slideDuration -= Time.deltaTime;
        }
        else
        {
            CharacterController.height = unCrouchHeight;
            slideDuration += Time.deltaTime;
            slideX = Input.GetAxisRaw("Horizontal");
            slideZ = Input.GetAxisRaw("Vertical");
            slideDir = Vector3.ClampMagnitude(transform.forward * slideZ + transform.right * slideX, 1f);
        }
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundcheck.position, groundistance, groundmask);

        if ((isGrounded || isClimbing) && velocity.y < 0) velocity.y = -2f;
        if (Input.GetButtonDown("Jump") && isGrounded) velocity.y = verticalJumpDir;
        velocity.y += gravity * Time.deltaTime;
        CharacterController.Move(velocity * Time.deltaTime);
    }

    void FixedUpdate()
    {
        newPos = transform.position - oldPos;
        oldPos = transform.position;
    }

    void Cannon()
    {
        if (knockBackDir.magnitude != 0f) CharacterController.Move(knockBackDir * knockBackSpeed * Time.deltaTime);
        if (knockBackSpeed > 0f) knockBackSpeed -= knockBackSlowSpeed * Time.deltaTime;
        if (knockBackSpeed < 0f) knockBackSpeed = 0f;
    }

    void WallRun()
    {
        isWallRunning = touchingWall && !isGrounded;
        if(isWallRunning)
        {
            WallJump();
            if (Input.GetKey(sprint)) CharacterController.Move(wallRunDir * wallRunSpeed * Time.deltaTime);
        }
        else wallRunDir = move;
        if (wallJumpMainDir.magnitude != 0f) CharacterController.Move(wallJumpMainDir * wallJumpSpeed * Time.deltaTime);
        if (wallJumpSpeed > 0f) wallJumpSpeed -= 4f * Time.deltaTime;
        if (wallJumpSpeed < 0f || isGrounded) wallJumpSpeed = 0f;
        if (wallJumpSpeed > 0f) speed = 1.5f; else speed = 6f;
    }

    void WallJump()
    {
        if (Input.GetButtonDown("Jump") && touchingWall)
        {
            wallJumpSpeed = 7f;
            velocity.y = verticalJumpDir;
            Debug.Log(wallJumpMainDir);
        }
    }

    void Vault()
    {

    }

    void Climb()
    {
        RaycastHit climbHit;
        if (Physics.Raycast(transform.position, transform.forward, out climbHit, .521f))
        {
            if (climbHit.transform.gameObject.CompareTag("Ledder") )
            {
                if (Input.GetKeyDown(interact))
                    isClimbing = !isClimbing;
                if (isClimbing)
                    CharacterController.Move(Vector3.up * climbSpeed * Time.deltaTime);
            }
            else isClimbing = false;
        }
        else isClimbing = false;
    }

    void OnTriggerStay(Collider other)
    {
        touchingWall = true;
        Debug.Log("TOUCHING WALL");
        RaycastHit wallHit;
        if (newPos.magnitude != 0f) wallRayDir = wallRunDir + wallJumpMainDir * wallJumpSpeed;
        if (Physics.Raycast(transform.position, wallRayDir, out wallHit))
            if (wallRunDir.magnitude >= .05f)
            {
                Vector3 newDir = Vector3.Reflect(wallRunDir, wallHit.normal).normalized;
                if (newDir.magnitude >= .05f) wallJumpMainDir = newDir;
                Debug.Log(newDir);
            }
		if (wallJumpSpeed <= 5f) wallJumpSpeed = 0f;
    }

    void OnTriggerExit(Collider other) => touchingWall = false;

    void CheckForFallDamage()
    {
        fallDuration += Time.deltaTime;
		if (fallDuration >= minFallDistance)
			if (isGrounded) health.SimpleTakeHealth(fallDuration * fallDamage);
    }
}