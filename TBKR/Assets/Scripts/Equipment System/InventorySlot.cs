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
        if (SlotNum < 2)
        {
            icon.sprite = Inventory.instance.ghostArm;
            icon.enabled = true;
        }
        else if (SlotNum > 1 && SlotNum < 4)
        {
            icon.sprite = Inventory.instance.ghostLeg;
            icon.enabled = true;
        }
        if (Item != null)
            icon.sprite = Item.icon;
    }

    public void AddItem (Item newItem)
    {
        Item = newItem;

        icon.enabled = true;
        removeButton.interactable = true;
        icon.sprite = newItem.icon;
    }

    public void ClearSlot()
    {
        Item = null;

        if (SlotNum > 3)
        {
            icon.sprite = null;
            icon.enabled = false;
        }
        else if (SlotNum < 2)
            icon.sprite = Inventory.instance.ghostArm;
        else if (SlotNum > 1)
            icon.sprite = Inventory.instance.ghostLeg;
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
        else if (Item && InBetween.instance.enabled)
        {
            Inventory.instance.SwapItem(SlotNum);
        }
    }
}
