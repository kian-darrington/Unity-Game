using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDataPersistance
{
    Rigidbody2D myBody;
    public CircleCollider2D[] myColliders;
    public CapsuleCollider2D[] sideColliders;
    SpriteRenderer mySprite;

    public Sword SwordRef;

    Sword NewSword;

    CircleCollider2D myCollider;
    CapsuleCollider2D sideCollider;

    public List<Item> items = new List<Item>();

    public List<Enemy> enemies;

    bool isGrounded = true;
    bool wallJump = false;
    bool onWall = false;
    bool inventoryOpen = false;
    bool raisedLimbs = true;
    bool headBonk = false;
    bool moveable = true;

    const float BaseSpeed = 1f, BaseJump = 15f, BaseWallJump = 10f, BaseAirTimeBuffer = 0.1f, BaseMaxVelocity = 8f, BaseWeaponCoolDown = 1f, BaseThrowingDistance = 6f;

    const float BaseDamage = 10;

    const float LimblessSpeed = 0.25f, LimblessJump = 5f, LimblessWallJump = 0f, LimblessMaxVelocity = 2.5f;

    float TimePassage = 0f;

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

    float WeaponCoolDown = BaseWeaponCoolDown, ThrowingDistance = 0;

    private float AirDrag = 0.99f;

    private float AirVelocity = 0f;

    private float Damage;

    public delegate void DamageChange(float Damage);
    public static event DamageChange DamageChangeInfo;

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
        float xMove = Input.GetAxisRaw("Horizontal");

        TimePassage += Time.deltaTime;

        // Controlls smooth horizontal movement
        if (((myBody.velocity.x < MaxVelocity && xMove > 0) || (myBody.velocity.x > -MaxVelocity && xMove < 0)) && xMove != 0 && !inventoryOpen && isGrounded)
        {
            myBody.AddForce(new Vector2(xMove * Speed, 0f), ForceMode2D.Impulse);
        }
        else if (((myBody.velocity.x < AirVelocity && xMove > 0) || (myBody.velocity.x > -AirVelocity && xMove < 0)) && xMove != 0 && !inventoryOpen && !isGrounded && moveable)
            myBody.AddForce(new Vector2(xMove * Speed * (4f / 5f), 0f), ForceMode2D.Impulse);
        else if (!isGrounded && xMove == 0f)
        {
            myBody.velocity = new Vector2(myBody.velocity.x * AirDrag, myBody.velocity.y);
        }
        if ((Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.E)) && isGrounded)
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

        // Sword flinging mechanism
        if (Input.GetKey(KeyCode.K) && ThrowingDistance > 0 && WeaponCoolDown - TimePassage <= 0f)
        {
            NewSword = Instantiate(SwordRef);
            if (!mySprite.flipX)
                NewSword.SetVelocity(ThrowingDistance + myBody.velocity.x, ThrowingDistance + myBody.velocity.y);
            else
                NewSword.SetVelocity(-ThrowingDistance + myBody.velocity.x, ThrowingDistance + myBody.velocity.y);
            NewSword.SetPosition(transform.position);

            TimePassage = 0f;
        }
    }

    // Ground and wall collision checks
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && myCollider.IsTouching(collision.collider))
        {
            StopCoroutine("DelayJumpGround");
            StopCoroutine("DelayControl");
            isGrounded = true;
            headBonk = false;
            moveable = true;
        }
        if (collision.gameObject.CompareTag("Ground") && sideCollider.IsTouching(collision.collider))
        {
            StopCoroutine("DelayJumpWall");
            StopCoroutine("DelayControl");
            onWall = true;
            wallJump = true;
            moveable = true;
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
        {
            headBonk = true;
            StartCoroutine("UnHeadBonk");
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.name.Contains("Swallow"))
            {
                if (collision.gameObject.transform.position.x - transform.position.x > 0)
                    myBody.velocity = new Vector2 (-enemies[0].knockBack, enemies[0].knockBack);
                else
                    myBody.velocity = new Vector2(enemies[0].knockBack, enemies[0].knockBack);
            }
            moveable = false;
            StartCoroutine("DelayControl");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            headBonk = false;
            StopCoroutine("UnHeadBonk");
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

    //Fixes knockback inablility
    IEnumerator DelayControl()
    {
        yield return new WaitForSeconds(0.25f);
        moveable = true;
    }

    IEnumerator UnHeadBonk()
    {
        yield return new WaitForSeconds(1.5f);
        headBonk = false;
    }

    void LimblessStatReset()
    {
        AirTimeBuffer = BaseAirTimeBuffer;
        Speed = LimblessSpeed;
        MaxVelocity = LimblessMaxVelocity;
        JumpForce = LimblessJump;
        WallJumpForce = LimblessWallJump;
        AirVelocity = MaxVelocity * (2f / 3f);
        ThrowingDistance = 0;
        Damage = 0;
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
        ThrowingDistance += item.throwingDistance;
        Damage += item.attackDamage;
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
                ThrowingDistance += BaseThrowingDistance / 2f;
                Damage += BaseDamage / 2f;
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
        AirVelocity = MaxVelocity * (2f / 3f);
        ColliderChange(hasLimbs);
        if (DamageChangeInfo != null)
            DamageChangeInfo(Damage);
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

    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }

    public void SaveData(ref GameData data)
    {
        Debug.Log("Called Save Position");
        data.playerPosition = this.transform.position;
        Debug.Log("Passed");
    }
}