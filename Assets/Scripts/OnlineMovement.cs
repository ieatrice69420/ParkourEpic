using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class OnlineMovement : MonoBehaviourPunCallbacks
{
    #region Variables

    [SerializeField]
    CharacterController CharacterController;
    public float speed = 12f, unCrouchSpeed = 12f, crouchSpeed = 6f, gravity = -20f, JumpHight = 3f, crouchHeight = 0.2f, unCrouchHeight = 0.5f, knockBackSpeed, knockBackSlowSpeed, slideSpeed, slideDuration;
    [SerializeField]
    Transform groundcheck;
    float groundistance = 0.2f;
    [SerializeField]
    LayerMask groundmask;
    Vector3 velocity;
    [SerializeField]
    bool isGrounded, isWallRunning, isSliding;
    public KeyCode crouch = KeyCode.LeftControl;
    [HideInInspector]
    public Vector3 knockBackDir;
    float x, z, xRaw, zRaw, wallJumpSpeed;
    Vector3 moveRaw, move, oldPos, newPos, wallJumpMainDir, slideDir;
    public Camera cam;
    public float mouseSensetivity = 100f;
    float Xrotation = 0,maxSlideDuration, slideX, slideZ;
    [HideInInspector]
    public bool canFall = true, canMove = true;

    #endregion

    void Start()
    {
        if (!photonView.IsMine)
        {
            Destroy(cam);
        }
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }


        #region Look

        float MouseX = Input.GetAxis("Mouse X") * mouseSensetivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * mouseSensetivity * Time.deltaTime;
        transform.Rotate(Vector3.up/*like to say 1, we use this because this veribale gets only vector 3 */ * MouseX);
        Xrotation -= MouseY;
        Xrotation = Mathf.Clamp(Xrotation, -90, 90);
        cam.transform.localRotation = Quaternion.Euler(Xrotation, 0, 0);

        #endregion

        #region Jump

        if (isGrounded)
        {
            xRaw = Input.GetAxisRaw("Horizontal");
            zRaw = Input.GetAxisRaw("Vertical");
            moveRaw = Vector3.ClampMagnitude(transform.right * xRaw + transform.forward * zRaw, 1f);
        }
        isGrounded = Physics.CheckSphere(groundcheck.position, groundistance, groundmask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = (float)(System.Math.Sqrt((double)(JumpHight * -2f * gravity)));
        }

        velocity.y += gravity * Time.deltaTime;
        if (canFall)
        {
            CharacterController.Move(velocity * Time.deltaTime);
        }

        #endregion

        #region Wasd

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        move = Vector3.ClampMagnitude(transform.right * x + transform.forward * z, 1f);

        if (canMove)
        {
            CharacterController.Move(move * speed * Time.deltaTime);

            if (Input.GetKey(crouch))
            {
                CharacterController.Move(move * speed / 2f * -1f * Time.deltaTime);
            }
        }
        #endregion

        #region Crouch

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

        #endregion


        /*      #region Cannon

              CharacterController.Move(knockBackDir * knockBackSpeed * Time.deltaTime);
              if (knockBackSpeed > 0f)
              {
                  knockBackSpeed -= knockBackSlowSpeed * Time.deltaTime;
              }

              if (knockBackSpeed < 0f)
              {
                  knockBackSpeed = 0f;
              }

              #endregion*/
    }
}