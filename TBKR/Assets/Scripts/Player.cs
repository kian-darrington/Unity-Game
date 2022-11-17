using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D myBody;
    CircleCollider2D myCollider;
    CapsuleCollider2D sideCollider;
    SpriteRenderer mySprite;


    bool isGrounded = true;
    bool wallJump = false;
    bool onWall = false;

    [SerializeField]
    private float _airTimeBuffer = 0.1f;
    public float AirTimeBuffer
    {
        get { return _airTimeBuffer; }
        set { _airTimeBuffer = value; }
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
    private float _wallJumpForce = 20f;
    public float WallJumpForce
    {
        get { return _wallJumpForce; }
        set { _wallJumpForce = value; }
    }

    [SerializeField]
    private float _maxVelocity = 20f;
    public float MaxVelocity
    {
        get { return _maxVelocity; }
        set { _maxVelocity = value; }
    }

    private float AirDrag = 0.99f;

    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CircleCollider2D>();
        sideCollider = GetComponent<CapsuleCollider2D>();
        mySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxis("Horizontal");

        // Controlls smooth horizontal movement
        if (((myBody.velocity.x < MaxVelocity && xMove > 0) || (myBody.velocity.x > -MaxVelocity && xMove < 0)) && Input.GetButton("Horizontal"))
        {
            myBody.AddForce(new Vector2(xMove * Speed, 0f), ForceMode2D.Impulse);
        }

        // Creates false air drag to make mid air controll easier
        if(!isGrounded && !Input.GetButton("Horizontal"))
        {
            myBody.velocity = new Vector2(myBody.velocity.x * AirDrag, myBody.velocity.y);
        }

        // Jumping mechanism
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            isGrounded = false;
            myBody.velocity = new Vector2(myBody.velocity.x, JumpForce);
        }
        // Wall jumping mechanism
        else if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && onWall && wallJump)
        {
            wallJump = false;
            myBody.velocity = new Vector2(myBody.velocity.x, WallJumpForce);
            isGrounded = false;
            StopCoroutine("DelayJumpGround");
        }

        // Sprite flipping according to button pressed
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            mySprite.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            mySprite.flipX = true;
        }
    }

    // Ground and wall collision checks
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

    // Ground and wall decollisions
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


    // Method of creating a delay for coyote time
    IEnumerator DelayJumpGround()
    {
        yield return new WaitForSeconds(AirTimeBuffer);
        isGrounded = false;
    }
    IEnumerator DelayJumpWall()
    {
        yield return new WaitForSeconds(AirTimeBuffer);
        onWall = false;
    }

}