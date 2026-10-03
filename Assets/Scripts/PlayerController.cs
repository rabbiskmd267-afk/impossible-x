
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed = 7f;
    public float jumpHeight = 2f;
    public float gravity = -22f;

    CharacterController controller;
    Vector3 velocity;
    Vector3 checkpoint;
    public Vector2 mobileInput;
    bool jumpRequested;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        checkpoint = transform.position;
    }

    void Update()
    {
        bool grounded = controller.isGrounded;

        if (grounded && velocity.y < 0)
            velocity.y = -2f;

        Vector2 input = mobileInput;

#if UNITY_EDITOR || UNITY_STANDALONE
        input += new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        if (Input.GetButtonDown("Jump"))
            jumpRequested = true;
#endif

        input = Vector2.ClampMagnitude(input, 1f);

        Vector3 move = new Vector3(input.x, 0, input.y);
        controller.Move(move * speed * Time.deltaTime);

        if (move.sqrMagnitude > 0.01f)
            transform.forward = move.normalized;

        if (grounded && jumpRequested)
            velocity.y = Mathf.Sqrt(
                jumpHeight * -2f * gravity
            );

        jumpRequested = false;
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (transform.position.y < -15f)
            Respawn();
    }

    public void SetMobileInput(Vector2 input)
    {
        mobileInput = Vector2.ClampMagnitude(input, 1f);
    }

    public void Jump()
    {
        jumpRequested = true;
    }

    public void SetCheckpoint(Vector3 point)
    {
        checkpoint = point;
    }

    public void Respawn()
    {
        controller.enabled = false;
        transform.position = checkpoint;
        controller.enabled = true;
        velocity = Vector3.zero;

        GameManager.Instance?.RegisterFail();
    }

    void OnControllerColliderHit(
        ControllerColliderHit hit
    )
    {
        if (hit.collider.CompareTag("Hazard"))
            Respawn();
    }
}
