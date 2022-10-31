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
            Debug.Log("jumped");
        }
        else if (Input.GetButtonDown("Jump") && onWall && wallJump && xMove != 0)
        {
            wallJump = false;
            myBody.AddForce(new Vector2((JumpForce / 2) * (0 + xMove), JumpForce), ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && myCollider.IsTouching(collision.collider))
        {
            isGrounded = true;
            Debug.Log("Is touching ground");
        }
        if (collision.gameObject.CompareTag("Ground") && sideCollider.IsTouching(collision.collider))
        {
            onWall = true;
            wallJump = true;
            Debug.Log("Is on wall");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !myCollider.IsTouching(collision.collider))
        {
            isGrounded = false;
        }
        if (collision.gameObject.CompareTag("Ground") && !sideCollider.IsTouching(collision.collider))
        {
            onWall = false;
        }
    }

}