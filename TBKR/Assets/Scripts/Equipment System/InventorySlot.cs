using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;

    int SlotNum;

    Item _item;
    public Item Item
    {
        get { return _item; }
        set { _item = value; }

    }

    public void Start()
    {
        for (int i = 0; i < Inventory.instance.items.Length; i++)
        {
            if (this == Inventory.instance.items[i])
                SlotNum = i;
        }
    }

    public void AddItem (Item newItem)
    {
        Item = newItem;

        icon.sprite = Item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        Item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.RemoveButton(SlotNum);
    }

    public void ClickedOn()
    {
        if (Item != null && !InBetween.instance.enabled)
        {
            InBetween.instance.item = Item;
            InBetween.instance.enabled = true;
            Inventory.instance.ClearSlot(SlotNum);
        }
        else if (InBetween.instance.enabled && Item == null)
        {
            Inventory.instance.AddItem(SlotNum);
        }
    }
}
