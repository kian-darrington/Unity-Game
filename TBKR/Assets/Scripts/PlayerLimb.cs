using UnityEngine;
using System;

public class PlayerLimb : MonoBehaviour
{
    Transform myTransform;
    SpriteRenderer mySprite;
    int LimbNum = 0, LeftRight = 1, direction = 1;
    float TimePassage = 0f;
    Item item;
    float previousDirection;

    public const float TimeMultiplier = 10f;
    public const float TimeLimit = 2f;
    public const float SwingAngle = 45f;

    Vector3 player; 

    // Start is called before the first frame update 0.4f, 0.25f, 1f
    void Start()
    {
        myTransform = GetComponent<Transform>();
        mySprite = GetComponent<SpriteRenderer>();

        player =  GameObject.FindWithTag("Player").transform.position;
        for (int i = 0; i < GameObject.FindWithTag("Player").GetComponentsInChildren<PlayerLimb>().Length; i++) {
            if (GameObject.FindWithTag("Player").GetComponentsInChildren<PlayerLimb>()[i] == this)
            {
                LimbNum = i;
                if (LimbNum == 0 || LimbNum == 2)
                    LeftRight = -1;
                break;
            }
        }

        mySprite.sprite = null;
        if (LimbNum < 2)
            myTransform.position = new Vector3(player.x + (0.4f * (float)LeftRight), player.y + 0.25f, 1f);
        else
            myTransform.position = new Vector3(player.x + (0.32f * (float)LeftRight), player.y - 1.3f, 1f);

        Inventory.inventoryChangedInfo += InventoryChanged;
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != direction && Input.GetAxisRaw("Horizontal") != 0)
        {
            direction = (int)Input.GetAxisRaw("Horizontal");
            SpriteUpdate();
        }
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            TimePassage += Time.deltaTime * TimeMultiplier;

            if (TimePassage < TimeLimit && LimbNum < 2)
                transform.rotation = Quaternion.Euler(Vector3.forward * -SwingAngle * (float)LeftRight);
            else if (TimePassage < (TimeLimit * 2f) && LimbNum < 2)
                transform.rotation = Quaternion.Euler(Vector3.forward * SwingAngle * (float)LeftRight);
            else if (TimePassage < TimeLimit)
                transform.rotation = Quaternion.Euler(Vector3.forward * (SwingAngle * 2f / 3f) * (float)LeftRight);
            else if (TimePassage < (TimeLimit * 2f))
                transform.rotation = Quaternion.Euler(Vector3.forward * -(SwingAngle * 2f / 3f) * (float)LeftRight);
            else
                TimePassage = 0f;
        }

        if ((Input.GetAxisRaw("Horizontal") == 0 || Input.GetAxisRaw("Horizontal") != previousDirection))
        {
            myTransform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }

        previousDirection = Input.GetAxisRaw("Horizontal");
    }

    void SpriteUpdate()
    {
        if (item != null)
            mySprite.sprite = item.icon;
        else
            mySprite.sprite = null;
        if (LimbNum < 2)
            mySprite.sortingOrder = -(LeftRight * (direction * (Math.Abs(direction) + 1)));
        else
            mySprite.sortingOrder = -(LeftRight * direction);
        if (direction == -1)
            mySprite.flipX = true;
        else
            mySprite.flipX = false;
        if (mySprite.sortingOrder < 0)
            mySprite.color = new Color(0.9f, 0.9f, 0.9f);
        else
            mySprite.color = Color.white;
    }

    void InventoryChanged()
    {
        if (Inventory.instance.items[LimbNum].Item != null)
        {
            item = Inventory.instance.items[LimbNum].Item;
            SpriteUpdate();
        }
        else
        {
            item = null;
            SpriteUpdate();
        }
    }
}
