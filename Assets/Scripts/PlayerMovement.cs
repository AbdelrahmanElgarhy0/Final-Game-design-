using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Forward Movement")]
    public float forwardSpeed = 6f;

    [Header("Jump")]
    public float jumpForce = 7f;

    [Header("Lane Movement")]
    public float laneDistance = 3f;
    public float laneSwitchSpeed = 10f;

    [Header("Endless Settings")]
    public float resetZ = -80f;   // 👈 RESET POINT

    Animator anim;
    Rigidbody rb;

    bool isGrounded = true;

    int currentLane = 1; // 0 = left, 1 = middle, 2 = right
    float startX;
    float startZ; // respawn Z

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        startX = transform.position.x;
        startZ = transform.position.z;

        currentLane = 1;
        anim.SetBool("isRunning", true);
    }

    void Update()
    {
        // ===== JUMP =====
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;

            Vector3 v = rb.linearVelocity;   // ✅ FIXED
            v.y = 0f;
            rb.linearVelocity = v;

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetTrigger("Jump");
        }

        // ===== LANE INPUT =====
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentLane > 0)
                currentLane--;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentLane < 2)
                currentLane++;
        }
    }

 void FixedUpdate()
{
    // Calculate forward movement
    float newZ = rb.position.z - forwardSpeed * Time.fixedDeltaTime;

    // 🚫 Prevent going past -80
    if (newZ <= resetZ)
    {
        // RESET immediately
        rb.position = new Vector3(
            startX,
            rb.position.y,
            startZ
        );

        currentLane = 1;
        isGrounded = true;
        return;
    }

    // Lane movement
    float targetX = startX + (currentLane - 1) * laneDistance;
    float newX = Mathf.Lerp(
        rb.position.x,
        targetX,
        laneSwitchSpeed * Time.fixedDeltaTime
    );

    rb.MovePosition(new Vector3(newX, rb.position.y, newZ));
}


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "street")
        {
            isGrounded = true;
        }
    }
}
