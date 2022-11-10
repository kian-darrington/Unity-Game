using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    bool IsColliding = false;

    public Item item;

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
        if (IsColliding && Input.GetKey(KeyCode.X))
        {
            PickUp();
        }
    }

    void PickUp()
    {
        // Add to inventory
        Debug.Log("Picking up " + item.name);
        Inventory.instance.Add(item);
        Destroy(gameObject);
    }
}
