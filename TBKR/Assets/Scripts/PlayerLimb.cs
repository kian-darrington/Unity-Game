using UnityEngine;
using System;

public class PlayerLimb : MonoBehaviour
{
    Transform myTransform;
    SpriteRenderer mySprite;
    int LimbNum = 0, direction = 1;
    bool LimbSwing = false;
    float TimePassage = 0f;
    Item item;
    bool rightKey = false, leftKey = false;

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
                Debug.Log("This Limb's Number is: " + i);
                break;
            }
        }

        mySprite.sprite = null;

        myTransform.position = new Vector3(player.x + (0.4f * (float)direction), player.y + 0.25f, 1f);

        Inventory.inventoryChangedInfo += InventoryChanged;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) && mySprite.sprite && !leftKey)
        {
            direction = -1;
            LimbSwing = true;
            rightKey = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && mySprite.sprite && !rightKey)
        {
            direction = 1;
            LimbSwing = true;
            leftKey = true;
        }


        if (LimbSwing)
        {
            TimePassage += Time.deltaTime;
            myTransform.rotation = new Quaternion(0f, 0f, 90f * (float)Math.Sin((double)TimePassage / (180 / Math.PI))  * (float)direction, 0f);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
            leftKey = false;
        if (Input.GetKeyUp(KeyCode.RightArrow))
            rightKey = false;

        if (leftKey && rightKey)
        {
            TimePassage = 0f;
            myTransform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            LimbSwing = false;
        }
    }

    void InventoryChanged()
    {
        if (Inventory.instance.items[LimbNum].Item != null)
        {
            item = Inventory.instance.items[LimbNum].Item;
            mySprite.sprite = item.icon;
        }
        else
            mySprite.sprite = null;
    }
}
