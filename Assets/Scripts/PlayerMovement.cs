using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    [Header("Game Over UI")]
public GameObject gameOverCanvas;
public TMPro.TextMeshProUGUI finalScoreText;

    [Header("Forward Movement")]
    public float forwardSpeed = 6f;

    [Header("Jump")]
    public float jumpForce = 7f;

    [Header("Lane Movement")]
    public float laneDistance = 3f;
    public float laneSwitchSpeed = 10f;

    [Header("Endless Settings")]
    public float resetZ = -80f;

    [Header("References")]
    public Transform obstaclesContainer;

    Animator anim;
    Rigidbody rb;

    bool isGrounded = false;
    bool isDead = false;

    int currentLane = 1; // 0 = left, 1 = middle, 2 = right
    float startX;
    float startZ;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        startX = transform.position.x;
        startZ = transform.position.z;

        anim.SetBool("isRunning", true);
         if (AudioManager.instance != null)
        AudioManager.instance.PlayMusicIfNotPlaying();
    }

    void Update()
    {
        if (isDead)
            return;

        // JUMP
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;

            Vector3 v = rb.linearVelocity;
            v.y = 0f;
            rb.linearVelocity = v;

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetTrigger("Jump");
        }

        // LANE LEFT
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentLane > 0)
                currentLane--;
        }

        // LANE RIGHT
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentLane < 2)
                currentLane++;
        }
    }

    void FixedUpdate()
    {
        if (isDead)
            return;

        float newZ = rb.position.z - forwardSpeed * Time.fixedDeltaTime;

        // ENDLESS RESET
        if (rb.position.z <= resetZ)
        {
            ClearObstacles();

            rb.position = new Vector3(startX, rb.position.y, startZ);
            currentLane = 1;
            isGrounded = true;
            return;
        }

        float targetX = startX + (currentLane - 1) * laneDistance;
        float newX = Mathf.Lerp(rb.position.x, targetX, laneSwitchSpeed * Time.fixedDeltaTime);

        rb.MovePosition(new Vector3(newX, rb.position.y, newZ));
    }

    // ✅ STABLE GROUND CHECK
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "street")
        {
            isGrounded = true;
        }
    }

    // ✅ OBSTACLE TRIGGER
    void OnTriggerEnter(Collider other)
    {
        if (isDead)
            return;

        Obstacle obstacle = other.GetComponent<Obstacle>();

        if (obstacle != null)
        {
            Debug.Log("HIT OBSTACLE");

            isDead = true;
            rb.linearVelocity = Vector3.zero;
           AudioManager.instance.StopMusic();   // ⛔ stop background music
AudioManager.instance.PlayHit();     // 🔊 play hit sound
anim.SetTrigger("Fall");


            anim.SetTrigger("Fall");
            Invoke(nameof(TriggerGameOver), 1f); // wait for fall animation
        }
    }
    public void RestartGame()
{
    Time.timeScale = 1f;   
    ScoreManager.instance.ResetScore();
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}


    void ClearObstacles()
    {
        if (obstaclesContainer == null)
            return;

        for (int i = obstaclesContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(obstaclesContainer.GetChild(i).gameObject);
        }
    }
    void TriggerGameOver()
{
    Time.timeScale = 0f; // stop game
    gameOverCanvas.SetActive(true);

    finalScoreText.text =
        "Score: " + ScoreManager.instance.GetFinalScore();
}

}
