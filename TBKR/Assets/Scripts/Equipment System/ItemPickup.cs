using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    bool IsColliding = false;

    public Item item;

    public SpriteRenderer Sprite;

    private void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
        Sprite.sprite = item.icon;
    }

    public void ItemSetup()
    {
        Sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsColliding = false;
        }
    }

    private void Update()
    {
        if (IsColliding && Input.GetKey(KeyCode.J))
        {
            PickUp();
        }
    }

    void PickUp()
    {
        bool wasPickedUp = Inventory.instance.ItemPickUp(item);
        if (wasPickedUp)
            Destroy(gameObject);
    }
}
