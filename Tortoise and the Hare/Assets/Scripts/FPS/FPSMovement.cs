using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] [Range(0, 1)] float acceleration = 0.75f;
    [SerializeField] float jumpHeight = 0.5f;

    [Header("Ground Settings")]
    [SerializeField] float groundedRadius = 0.05f;
    [SerializeField] float groundedDistance = 0.5f;
    [SerializeField] LayerMask groundLayer;

    Collider col;
    Rigidbody rig;
    float gravity = Physics.gravity.y;
    Vector3 velocity;

    bool IsGrounded
    {
        get
        {
            Vector3 extents = col.bounds.extents / 2;
            extents.y = groundedRadius;
            return Physics.CheckBox(transform.position + (Vector3.down * groundedDistance), extents, Quaternion.identity ,groundLayer, QueryTriggerInteraction.Ignore);
        }
    }

    private void Start()
    {
        col = GetComponent<Collider>();
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

        //If can jump and want to jump
        if (Input.GetButton("Jump") && IsGrounded)
        {
            //Gain velocity proportional to jump height
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //Apply gravity over time
        velocity.y += gravity * Time.deltaTime;

        //If landed on the ground
        if (IsGrounded && velocity.y < 0)
        {
            //Reset the velocity from gravity
            velocity.y = 0;
        }

        //Calculate movement direction
        Vector3 move = Vector3.ClampMagnitude((transform.right * horiz) + (transform.forward * vert), 1);

        //Apply movement
        rig.velocity = Vector3.Lerp(rig.velocity, (move * moveSpeed) + velocity, acceleration);
    }
}