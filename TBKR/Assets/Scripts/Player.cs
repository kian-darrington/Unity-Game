using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D myBody;
    CircleCollider2D myCollider;
    CapsuleCollider2D sideCollider;

    bool isGrounded = true;
    bool wallJump = false;
    bool onWall = false;

    [SerializeField]
    private float _timeBuffer = 0.1f;
    public float TimeBuffer
    {
        get { return _timeBuffer; }
        set { _timeBuffer = value; }
    }

    [SerializeField]
    private float _speed = 10f;
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    [SerializeField]
    private float _jumpForce = 20f;
    public float JumpForce
    {
        get { return _jumpForce; }
        set { _jumpForce = value; }
    }

    [SerializeField]
    private float _maxVelocity = 20f;
    public float MaxVelocity
    {
        get { return _maxVelocity; }
        set { _maxVelocity = value; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CircleCollider2D>();
        sideCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal");

        if ((myBody.velocity.x < MaxVelocity && xMove > 0) || (myBody.velocity.x > -MaxVelocity && xMove < 0))
        {
            myBody.AddForce(new Vector2(xMove * Speed, 0f), ForceMode2D.Impulse);
        }


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        }
        else if (Input.GetButtonDown("Jump") && onWall && wallJump)
        {
            wallJump = false;
            myBody.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && myCollider.IsTouching(collision.collider))
        {
            StopCoroutine("DelayJumpGround");
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Ground") && sideCollider.IsTouching(collision.collider))
        {
            StopCoroutine("DelayJumpWall");
            onWall = true;
            wallJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !myCollider.IsTouching(collision.collider))
        {
            StartCoroutine("DelayJumpGround");
        }
        if (collision.gameObject.CompareTag("Ground") && !sideCollider.IsTouching(collision.collider))
        {
            StartCoroutine("DelayJumpWall");
        }
    }
    IEnumerator DelayJumpGround()
    {
        yield return new WaitForSeconds(TimeBuffer);
        isGrounded = false;
    }
    IEnumerator DelayJumpWall()
    {
        yield return new WaitForSeconds(TimeBuffer);
        onWall = false;
    }

}