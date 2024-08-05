using UnityEngine;

public class PlayeMovement : MonoBehaviour
{
    [SerializeField] float speed;

    Rigidbody2D rb;
    [SerializeField] Animator characterAnim;

    void Start()
    {
        Player.Save += SpeedSave;

        if (PlayerPrefs.HasKey(PrefsKeys.playerMovementSpeed))
        {
            speed = PlayerPrefs.GetFloat(PrefsKeys.playerMovementSpeed);
        }
        else
        {
            speed = 2f;
            PlayerPrefs.SetFloat(PrefsKeys.playerMovementSpeed, speed);
        }

        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        Rotate();
        Move();
    }

    private void SpeedSave()
    {
        PlayerPrefs.SetFloat (PrefsKeys.playerMovementSpeed, speed);
    }

    private void Rotate()
    {
        if (!characterAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                transform.localScale = Vector3.one;
            }
            else
            {
                if (Input.GetAxis("Horizontal") > 0)
                    transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
    private void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        if (new Vector2(inputX, inputY) != Vector2.zero)
            characterAnim.SetBool("isMoving", true);
        else characterAnim.SetBool("isMoving", false);
        rb.velocity = new Vector2(inputX, inputY) * speed;
    }
}
