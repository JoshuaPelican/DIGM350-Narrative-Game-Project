using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 6;
    [SerializeField] float jumpHeight = 0.5f;

    [Header("Ground Settings")]
    [SerializeField] float groundedRadius = 0.25f;
    [SerializeField] float groundedDistance = 0.65f;
    [SerializeField] LayerMask groundLayer;

    [Header("Testing Settings - Please Remove!")]
    [SerializeField] Condition hasJumped;

    Rigidbody rig;
    float gravity = Physics.gravity.y;
    Vector3 velocity;

    bool IsGrounded
    {
        get
        {
            return Physics.CheckSphere(transform.position + (Vector3.down * groundedDistance), groundedRadius, groundLayer, QueryTriggerInteraction.Ignore);
        }
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        //Gets both input axis
        float horiz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        //If landed on the ground
        if (IsGrounded && velocity.y < 0)
        {
            //Reset the velocity from gravity
            velocity.y = 0;
        }

        //If can jump and want to jump
        if (Input.GetButton("Jump") && IsGrounded)
        {
            //Gain velocity proportional to jump height
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            //Testing
            hasJumped.Value = true;
        }

        //Apply gravity over time
        velocity.y += gravity * Time.deltaTime;

        //Calculate movement direction
        Vector3 move = Vector3.Normalize((transform.right * horiz) + (transform.forward * vert)) + velocity;
        //Calculate movement speed
        float speed = moveSpeed;

        //Apply movement
        rig.velocity = (move * speed);
    }
}