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

    public void ClearSlot(int SlotNum)
    {
        items[SlotNum].ClearSlot();
        items[SlotNum].Item = null;
    }

    public void AddItem(int SlotNum)
    {
        items[SlotNum].AddItem(InBetween.instance.item);
        InBetween.instance.enabled = false;
    }

    public bool ItemPickUp(Item item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].Item == null)
            {
                items[i].AddItem(item);
                return true;
                Debug.Log("Picked up Item");
            }
        }
        Debug.Log("Unable to pick up Item");
        return false;
    }
}
