using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 6;
    [SerializeField] float jumpHeight = 0.5f;

    CharacterController controller;
    float gravity = Physics.gravity.y;
    Vector3 velocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        //Gets both input axis
        float horiz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        //Checks if grounded
        bool isGrounded = controller.isGrounded;

        //If landed on the ground
        if (isGrounded && velocity.y < 0)
        {
            //Reset slope limit to normal
            controller.slopeLimit = 45f;

            //Reset the velocity from gravity
            velocity.y = 0;
        }

        //If can jump and want to jump
        if (Input.GetButton("Jump") && isGrounded)
        {
            //Adjust slope limit to prevent stutter when jumping onto objects
            controller.slopeLimit = 100f;

            //Gain velocity proportional to jump height
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //When hitting a ceiling
        if ((controller.collisionFlags & CollisionFlags.Above) != 0)
        {
            //Resets velocity to prevent ceiling sticking
            velocity.y = -2f;
        }

        //Apply gravity over time
        velocity.y += gravity * Time.deltaTime;

        //Calculate movement direction
        Vector3 move = Vector3.Normalize((transform.right * horiz) + (transform.forward * vert)) + velocity;
        //Calculate movement speed
        float speed = moveSpeed * Time.deltaTime;

        //Apply movement
        controller.Move(move * speed);
    }
}