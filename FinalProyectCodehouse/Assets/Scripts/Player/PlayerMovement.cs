using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float movementForce = 3f;
    [SerializeField] private float jump = 6f;
    private float delayJump = 1f;
    [SerializeField] private float maxSpeed = 5f;
    private bool canJump = true;
    private bool inDelayJump = false;
    private float cameraAxisX;
    public bool CanJump { get => canJump; set => canJump = value; }
    [SerializeField] private Rigidbody myRigidbody;
    public Rigidbody MyRigidbody { get => myRigidbody; set => myRigidbody = value; }
    public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
    [SerializeField] private Vector3 playerDirection;

    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        RotatePlayer();
        bool forward = Input.GetKeyDown(KeyCode.W);
        bool back = Input.GetKeyDown(KeyCode.S);
        bool left = Input.GetKeyDown(KeyCode.A);
        bool right = Input.GetKeyDown(KeyCode.D);
        if ((forward || back || left || right) && !IsAnimation("IsJumping"))
        {
            playerAnimator.SetBool("IsIdle", false);
            playerAnimator.SetBool("IsRunning", true);
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            playerAnimator.SetBool("IsRunning", false);
            if (!IsAnimation("IsIdle") && !IsAnimation("IsJumping")) playerAnimator.SetBool("IsIdle",true);
        }
        playerDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) playerDirection += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) playerDirection += Vector3.back;
        if (Input.GetKey(KeyCode.D)) playerDirection += Vector3.right;
        if (Input.GetKey(KeyCode.A)) playerDirection += Vector3.left;
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        playerAnimator.SetBool("IsJumping", false);
    }

    private void OnTriggerExit(Collider other)
    {
        playerAnimator.SetBool("IsJumping", true);
    }

    private void FixedUpdate()
    {
        Move();
    }
    private bool IsAnimation(string animName)
    {
        return playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(animName);
    }

    private void Move()
    {
        if (playerDirection != Vector3.zero && MyRigidbody.velocity.magnitude < MaxSpeed)
        {
            MyRigidbody.AddForce(transform.TransformDirection(playerDirection) * movementForce, ForceMode.Impulse);
        }

        if (!canJump && !inDelayJump)
        {
            MyRigidbody.AddForce(Vector3.up * jump, ForceMode.Impulse);
            inDelayJump = true;
            Invoke("DelayJump", delayJump);
        }
    }

    private void DelayJump()
    {
        inDelayJump = false;
        canJump = true;
    }

    public void RotatePlayer()
    {
        cameraAxisX += Input.GetAxis("Mouse X");
        Quaternion newRotation = Quaternion.Euler(0, cameraAxisX, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 2.5f * Time.deltaTime);
    }
}
