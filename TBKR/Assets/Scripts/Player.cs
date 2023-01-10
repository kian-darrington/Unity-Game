using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D myBody;
    public CircleCollider2D[] myColliders;
    public CapsuleCollider2D[] sideColliders;
    SpriteRenderer mySprite;

    CircleCollider2D myCollider;
    CapsuleCollider2D sideCollider;

    public List<Item> items = new List<Item>();

    bool isGrounded = true;
    bool wallJump = false;
    bool onWall = false;
    bool inventoryOpen = false;
    bool raisedLimbs = true;
    bool headBonk = false;

    const float BaseSpeed = 1f, BaseJump = 15f, BaseWallJump = 10f, BaseAirTimeBuffer = 0.1f, BaseMaxVelocity = 8f;

    const float LimblessSpeed = 0.25f, LimblessJump = 5f, LimblessWallJump = 0f, LimblessMaxVelocity = 2.5f;

    [SerializeField]
    private float _airTimeBuffer = BaseAirTimeBuffer;
    public float AirTimeBuffer
    {
        get { return _airTimeBuffer; }
        set { _airTimeBuffer = value; }
    }

    [SerializeField]
    private float _speed = LimblessSpeed;
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    [SerializeField]
    private float _jumpForce = LimblessJump;
    public float JumpForce
    {
        get { return _jumpForce; }
        set { _jumpForce = value; }
    }

    [SerializeField]
    private float _wallJumpForce = LimblessWallJump;
    public float WallJumpForce
    {
        get { return _wallJumpForce; }
        set { _wallJumpForce = value; }
    }

    [SerializeField]
    private float _maxVelocity = LimblessMaxVelocity;
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
        sideColliders = GetComponents<CapsuleCollider2D>();
        mySprite = GetComponent<SpriteRenderer>();

        ColliderChange(false);

        LimblessStatReset();

        Inventory.inventoryChangedInfo += InventoryChanged;

        for (int i = 0; i < 4; i++)
            items.Add(new Item());
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxis("Horizontal");

        // Controlls smooth horizontal movement
        if (((myBody.velocity.x < MaxVelocity && xMove > 0) || (myBody.velocity.x > -MaxVelocity && xMove < 0)) && Input.GetButton("Horizontal") && !inventoryOpen)
        {
            myBody.AddForce(new Vector2(xMove * Speed, 0f), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.I) && isGrounded)
        {
            if (HoldingScript.instance.isActiveAndEnabled && !InBetween.instance.enabled)
            {
                HoldingScript.instance.gameObject.SetActive(false);
                inventoryOpen = false;
            }
            else
            {
                HoldingScript.instance.gameObject.SetActive(true);
                Inventory.instance.GetPlayerPos(transform.position);
                inventoryOpen = true;
            }
        }

        // Creates false air drag to make mid air controll easier
        if(!isGrounded && !Input.GetButton("Horizontal"))
        {
            myBody.velocity = new Vector2(myBody.velocity.x * AirDrag, myBody.velocity.y);
        }

        // Jumping mechanism
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isGrounded && !inventoryOpen)
        {
            isGrounded = false;
            myBody.velocity = new Vector2(myBody.velocity.x, JumpForce);
        }
        // Wall jumping mechanism
        else if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && !headBonk && onWall && wallJump && !inventoryOpen)
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
            headBonk = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            headBonk = true;
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

    void LimblessStatReset()
    {
        AirTimeBuffer = BaseAirTimeBuffer;
        Speed = LimblessSpeed;
        MaxVelocity = LimblessMaxVelocity;
        JumpForce = LimblessJump;
        WallJumpForce = LimblessWallJump;
    }

    void AddLegStats(Item item)
    {
        JumpForce += item.jumpForce;
        MaxVelocity += item.maxVelocity;
        Speed += item.speed;
    }

    void AddArmStats(Item item)
    {
        WallJumpForce += item.wallJumpForce;
    }

    void InventoryChanged()
    {
        LimblessStatReset();
        List<Item> temp = items;
        bool hasLimbs = false;
        for (int i = 0; i < 4; i++)
        {
            items[i] = Inventory.instance.items[i].Item;
            if (i < 2 && items[i] != null)
            {
                WallJumpForce += BaseWallJump / 2f;
                AddArmStats(items[i]);
            }
            else if (i > 1 && items[i] != null)
            {
                JumpForce += (BaseJump - LimblessJump) / 2f;
                MaxVelocity += (BaseMaxVelocity - LimblessMaxVelocity) / 2f;
                Speed += (BaseSpeed - LimblessSpeed) / 2f;
                AddLegStats(items[i]);
                hasLimbs = true;
            }
        }
        ColliderChange(hasLimbs);
    }

    void ColliderChange(bool hasLimbs)
    {
        if (hasLimbs && !raisedLimbs)
        {
            myColliders[0].enabled = false;
            sideColliders[0].enabled = false;
            transform.position = new Vector3(transform.position.x, transform.position.y + 1f);
            myColliders[2].enabled = true;
            sideColliders[1].enabled = true;
            myCollider = myColliders[2];
            sideCollider = sideColliders[1];
            raisedLimbs = true;
        }
        else if (raisedLimbs && !hasLimbs)
        {
            myColliders[0].enabled = true;
            sideColliders[0].enabled = true;
            myColliders[2].enabled = false;
            sideColliders[1].enabled = false;
            myCollider = myColliders[0];
            sideCollider = sideColliders[0];
            raisedLimbs = false;
        }
    }
}