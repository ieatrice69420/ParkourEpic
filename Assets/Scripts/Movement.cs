using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    CharacterController CharacterController;
    public float speed = 12f, unCrouchSpeed = 12f, crouchSpeed = 6f, gravity = -20f, JumpHight = 3f, crouchHeight = 0.2f, unCrouchHeight = 0.5f, knockBackSpeed, knockBackSlowSpeed, slideSpeed, slideDuration, wallRunSpeed;
    [SerializeField]
    Transform groundcheck, cam, vaultPoint, vaultEndPoint;
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
    bool isVaulting, isVaultingUp, isVaultingForward;
    public float vaultSpeed, climbSpeed = 0.5f;
    [HideInInspector]
    public bool isClimbing;
    float verticalJumpDir;
    [HideInInspector]
    public bool touchingWall;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        verticalJumpDir = (float)(Math.Sqrt((double)(JumpHight * -2f * gravity)));
    }

    void Update()
    {
        if (!isVaulting)
        {
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
        }
        Vault();
    }

    void Wasd()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        move = Vector3.ClampMagnitude(transform.right * x + transform.forward * z, 1f);
        CharacterController.Move(move * speed * Time.deltaTime);
    }

    void Sprint()
    {
        if (Input.GetKey(sprint))
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
            move = Vector3.ClampMagnitude(transform.right * x + transform.forward * z, 1f);
            CharacterController.Move(move * speed / 3 * Time.deltaTime);
        }
    }

    void Slide()
    {
        if (Input.GetKeyDown(crouch))
        {
            CharacterController.height = crouchHeight;
        }

        if (Input.GetKeyUp(crouch))
        {
            CharacterController.height = unCrouchHeight;
        }

        isSliding = isGrounded && Input.GetKey(crouch) && slideDuration > 0f;
        if (slideDuration < 0f)
        {
            slideDuration = 0f;
        }
        if (slideDuration > maxSlideDuration)
        {
            slideDuration = maxSlideDuration;
        }

        if (isSliding)
        {
            CharacterController.Move(slideDir * slideSpeed * Time.deltaTime);
            slideDuration -= Time.deltaTime;
        }
        else
        {
            slideDuration += Time.deltaTime;
            slideX = Input.GetAxisRaw("Horizontal");
            slideZ = Input.GetAxisRaw("Vertical");
            slideDir = Vector3.ClampMagnitude(transform.forward * slideZ + transform.right * slideX, 1f);
        }
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundcheck.position, groundistance, groundmask);
        if ((isGrounded || isClimbing) && velocity.y < 0)
            velocity.y = -2f;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = verticalJumpDir;
        }
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
        CharacterController.Move(knockBackDir * knockBackSpeed * Time.deltaTime);
        if (knockBackSpeed > 0f)
        {
            knockBackSpeed -= knockBackSlowSpeed * Time.deltaTime;
        }

        if (knockBackSpeed < 0f)
        {
            knockBackSpeed = 0f;
        }
    }

    void WallRun()
    {
        Debug.Log(isWallRunning);
        isWallRunning = touchingWall && !isGrounded;
        if(isWallRunning)
        {
            WallJump();
            if (Input.GetKey(sprint)) CharacterController.Move(wallRunDir * wallRunSpeed * Time.deltaTime);
        }
        else wallRunDir = move;
        CharacterController.Move(wallJumpMainDir * wallJumpSpeed * Time.deltaTime);
        if (wallJumpSpeed > 0f) wallJumpSpeed -= 4f * Time.deltaTime;
        if (wallJumpSpeed < 0f || isGrounded) wallJumpSpeed = 0f;
        if (wallJumpSpeed > 0f) speed = 1.5f; else speed = 6f;
    }

    void WallJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            wallJumpSpeed = 7f;
            velocity.y = verticalJumpDir;
            Debug.Log("fadfafasf");
        }
    }

    void Vault()
    {
        RaycastHit vaultHit, vaultEndHit;
        if (Physics.Raycast(vaultPoint.position, transform.forward, out vaultHit, .6f, groundmask) && !Physics.Raycast(vaultEndPoint.position, transform.forward, out vaultEndHit, .6f, groundmask))
            if (vaultHit.transform.CompareTag("Vault"))
                if (Input.GetKeyDown(interact))
                    isVaultingForward = true;
        isVaulting = isVaultingUp || isVaultingForward;
        if (isVaulting)
            VaultUp();
    }

    void VaultUp()
    {
        CharacterController.Move(Vector3.up * vaultSpeed * Time.deltaTime);
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
            else
                isClimbing = false;
        }
        else
            isClimbing = false;
    }

    /// <summary>
    /// OnCollisionStay is called once per frame for every collider/rigidbody
    /// that is touching rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnTriggerStay(Collider other)
    {
        touchingWall = true;
        Debug.Log("TOUCHING WALL");
        RaycastHit wallHit;
        if (Physics.Raycast(transform.position, (other.transform.position - transform.position), out wallHit))
            if (wallRunDir.magnitude >= .05f)
                wallJumpMainDir = Vector3.Reflect(wallRunDir, wallHit.normal).normalized;
    }

    /// <summary>
    /// OnCollisionExit is called when this collider/rigidbody has
    /// stopped touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnTriggerExit(Collider other)
    {
        touchingWall = false;
    }
}
