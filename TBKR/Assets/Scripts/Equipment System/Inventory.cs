using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one istance of Inventory found!");
            return;
        }
        instance = this;

        items = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    #endregion
    public Transform itemsParent;

    public InventorySlot[] items;

    public ItemPickup ItemReference;

    ItemPickup NewDroppedItem;

    Vector3 PlayerPos;

    public delegate void InventoryChanged();
    public static event InventoryChanged inventoryChangedInfo;

    public void ClearSlot(int SlotNum)
    {
        items[SlotNum].ClearSlot();
        items[SlotNum].Item = null;

        if (inventoryChangedInfo != null && SlotNum < 4)
            inventoryChangedInfo();
    }

    public void AddItem(int SlotNum)
    {
        items[SlotNum].AddItem(InBetween.instance.item);

        InBetween.instance.enabled = false;

        if (inventoryChangedInfo != null && SlotNum < 4)
            inventoryChangedInfo();
    }

    public bool ItemPickUp(Item item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].Item == null)
            {
                items[i].AddItem(item);

                if (inventoryChangedInfo != null && i < 4)
                    inventoryChangedInfo();
                return true;
            }
        }
        Debug.Log("Unable to pick up Item");
        return false;
    }

    public void GetPlayerPos(Vector3 pos)
    {
        PlayerPos = pos;
    }

    public void RemoveButton(int SlotNum)
    {
        NewDroppedItem = Instantiate(ItemReference);
        NewDroppedItem.item = items[SlotNum].Item;
        NewDroppedItem.transform.position = new Vector3(PlayerPos.x, PlayerPos.y + 0.5f);
        NewDroppedItem.Sprite.sprite = NewDroppedItem.item.icon;

        ClearSlot(SlotNum);

        if (inventoryChangedInfo != null && SlotNum < 4)
            inventoryChangedInfo();
    }
}
