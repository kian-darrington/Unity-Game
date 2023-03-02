using UnityEngine.UI;
using UnityEngine;

public class Inventory : MonoBehaviour, IDataPersistance
{
    #region Singleton
    public static Inventory instance;

    public Sprite ghostArm, ghostLeg;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one istance of Inventory found!");
            return;
        }
        instance = this;

        items = itemsParent.GetComponentsInChildren<InventorySlot>();
        InventorySlot.inventoryLoadInfo += LoadItem;
    }

    #endregion
    public Transform itemsParent;//Inventory.instance.items[i].item

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

    public void AddItem(int SlotNum, Item item)
    {
        items[SlotNum].AddItem(item);

        InBetween.instance.enabled = false;

        if (inventoryChangedInfo != null && SlotNum < 4)
            inventoryChangedInfo();
    }

    public void SwapItem(int SlotNum)
    {
        Item temp = InBetween.instance.item;
        InBetween.instance.item = items[SlotNum].Item;
        InBetween.instance.UpdateSprite();

        ClearSlot(SlotNum);

        items[SlotNum].AddItem(temp);

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
                items[i].icon.sprite = item.icon;
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
        NewDroppedItem.ItemSetup();
        NewDroppedItem.item = items[SlotNum].Item;

        ClearSlot(SlotNum);

        NewDroppedItem.transform.position = new Vector3(PlayerPos.x, PlayerPos.y + 0.5f);
        NewDroppedItem.Sprite.sprite = NewDroppedItem.item.icon;
        NewDroppedItem.Sprite.enabled = true;

        if (inventoryChangedInfo != null && SlotNum < 4)
            inventoryChangedInfo();
    }

    public Item[] saveditems = new Item[12];

    public void LoadData(GameData data)
    {
        if (data.itemsaves != null)
        {
            saveditems = data.itemsaves;
        }
        else
            Debug.Log("AW FRICK");
    }

    public void SaveData(ref GameData data)
    {
        for (int i = 0; i < instance.items.Length; i++)
        {
             data.itemsaves[i] = instance.items[i].Item;
        }
    }

    public Item LoadItem(int SlotNum)
    {
        return saveditems[SlotNum];
    }

}
