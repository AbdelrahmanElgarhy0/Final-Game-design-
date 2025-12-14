using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 6f;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isRunning", true);
    }

    void Update()
    {
        // Move the player forward every frame
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }
}
