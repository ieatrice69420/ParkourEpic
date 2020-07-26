using static System.Math;
using System.Collections;
using UnityEngine;
using static UnityEngine.Mathf;

public class Movement : MonoBehaviour
{
    [SerializeField]
    CharacterController CharacterController;
    public float speed = 12f, unCrouchSpeed = 12f, crouchSpeed = 6f, gravity = -20f, JumpHight = 3f, crouchHeight = 0.2f, unCrouchHeight = 0.5f, knockBackSpeed, knockBackSlowSpeed, slideSpeed, slideDuration, wallRunSpeed;
    [SerializeField]
    Transform groundcheck, cam;
    [SerializeField]
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
    [SerializeField]
    new Rigidbody rigidbody;
    [SerializeField]
    float cielingDistance;
    Vector3 oldNormal;
    [SerializeField]
    float rollDur, rollSpeed, rollDelay;
    bool isRolling;
    Vector3 rollDir;
    bool canRoll = true;
    [SerializeField]
    Push push;

    bool isFalling()
    {
        if (groundedTime > 0.4f || velocity.y > 0f) return false;
        if (!isGrounded && velocity.y < 0f) return true;
        return true;
    }

    void Start() => verticalJumpDir = (float)(Sqrt((double)(JumpHight * -2f * gravity)));

    void Update()
    {
        if (Approximately(Time.timeScale, 0f)) return;
        newPos = transform.position - oldPos;
        if (isGrounded) RollInput();
        if (isRolling) return;
        Jump();
        if (!(isWallRunning && Input.GetKey(sprint)))
        {
            Wasd();
            Slide();
        }
        Cannon();
        WallRun();
        Climb();
        Vault();
        FallOffCieling();
        FinalMove();
        if (isFalling()) CheckForFallDamage();
        else fallDuration = 0f;
        if (isGrounded) groundedTime += Time.deltaTime;
        else groundedTime = 0f;
        oldPos = transform.position;
    }

    void Wasd()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        move = Vector3.ClampMagnitude(transform.right * x + transform.forward * z, 1f);
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
    }

    void Cannon()
    {
        if (knockBackDir.magnitude >= .05f) CharacterController.Move(knockBackDir * knockBackSpeed * Time.deltaTime);
        if (knockBackSpeed > 0f) knockBackSpeed -= knockBackSlowSpeed * Time.deltaTime;
        if (knockBackSpeed < 0f) knockBackSpeed = 0f;
    }

    void WallRun()
    {
        isWallRunning = touchingWall && !isGrounded;
        if (isWallRunning)
        {
            WallJump();
            if (Input.GetKey(sprint)) CharacterController.Move(wallRunDir * wallRunSpeed * Time.deltaTime);
        }
        else wallRunDir = move;
        if (wallJumpSpeed > 0f) wallJumpSpeed -= 4f * Time.deltaTime;
        if (wallJumpSpeed < 0f || isGrounded) wallJumpSpeed = 0f;
        speed = wallJumpSpeed > 0f ? 1.5f : 6f;
    }

    void WallJump()
    {
        if (Input.GetButtonDown("Jump") && touchingWall)
        {
            wallJumpSpeed = 7f;
            velocity.y = verticalJumpDir;
        }
    }

    void Vault()
    {

    }

    void FixedUpdate()
    {
        if (rigidbody.IsSleeping()) rigidbody.WakeUp();
    }

    void Climb()
    {
        RaycastHit climbHit;
        if (Physics.Raycast(transform.position, transform.forward, out climbHit, .521f))
        {
            if (climbHit.transform.gameObject.CompareTag("Ledder"))
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

    void OnCollisionStay(Collision other)
    {
        Vector3 newNormal = other.GetContact(0).normal;
        if (newNormal != oldNormal) wallRunDir = move;
        Vector3 newDir = Vector3.zero;
        touchingWall = true;

        if (wallRunDir.magnitude >= .05f)
        {
            if (other.contactCount > 2) newDir = (wallRunDir * -1f).normalized;
            else newDir = Vector3.Reflect(wallRunDir, other.GetContact(0).normal).normalized;

            bool hasntHitSameWall;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, newDir, out hit))
                if (hit.transform.gameObject == other.gameObject) hasntHitSameWall = false;
                else hasntHitSameWall = true;
            else hasntHitSameWall = true;

            if (newDir.magnitude >= .05f || hasntHitSameWall) wallJumpMainDir = newDir;
            else wallJumpMainDir = (transform.position - other.GetContact(0).point).normalized;
        }
        else wallJumpMainDir = (transform.position - other.GetContact(0).point).normalized;

        if (wallJumpSpeed <= 5f) wallJumpSpeed = 0f;
        oldNormal = other.GetContact(0).normal;
    }

    void OnCollisionExit() => touchingWall = false;

    void CheckForFallDamage()
    {
        fallDuration += Time.deltaTime;
        if (fallDuration >= minFallDistance && isGrounded) health.SimpleTakeHealth(fallDuration * fallDamage);
    }

    void FallOffCieling()
    {
        if (Physics.Raycast(transform.position, Vector3.up, cielingDistance, groundmask)) velocity.y = Clamp(velocity.y, -100f, -1f);
    }

    void RollInput()
    {
        if (isRolling) CharacterController.Move(rollDir * rollSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.E) && canRoll)
        {
            StartCoroutine(Roll());
            StartCoroutine(RollDelay());
        }
    }

    IEnumerator Roll()
    {
        rollDir = move.normalized;
        isRolling = true;
        yield return new WaitForSeconds(rollDur);
        isRolling = false;
    }

    IEnumerator RollDelay()
    {
        canRoll = false;
        yield return new WaitForSeconds(rollDelay);
        canRoll = true;
    }

    void FinalMove()
    {
        Vector3 finalMove = (move * speed) + (velocity) + (wallJumpMainDir * wallJumpSpeed) + (push.ropeNewPos * push.jumpSpeed * (push.isSwinging ? 0f : 1f));
        CharacterController.Move(finalMove * Time.deltaTime);
    }
}