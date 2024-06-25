using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Singleton;
    public static InventoryItem carriedItem;

    [SerializeField] InventorySlot[] inventorySlots;
    [SerializeField] InventorySlot[] hotbarSlots;

    [SerializeField] InventorySlot[] equipmentSlots;

    [SerializeField] Transform draggablesTransform;
    [SerializeField] InventoryItem itemPrefab;

    [Header("Item List")]
    [SerializeField] Item[] items;

    [Header("Debug")]
    [SerializeField] Button giveItemBtn;

    void Awake()
    {
        Singleton = this;
        if (giveItemBtn != null)
        {
            giveItemBtn.onClick.AddListener(delegate { SpawnRandomInventoryItem(); });
        }
    }

    void Update()
    {
        if (carriedItem == null) return;

        carriedItem.transform.position = Input.mousePosition;
    }

    public void SetCarriedItem(InventoryItem item)
    {
        if (carriedItem != null)
        {
            if (item.activeSlot.myTag != SlotTag.None && item.activeSlot.myTag != carriedItem.myItem.itemTag) return;
            item.activeSlot.SetItem(carriedItem);
        }

        if (item.activeSlot.myTag != SlotTag.None)
        {
            EquipEquipment(item.activeSlot.myTag, null);
        }

        carriedItem = item;
        carriedItem.canvasGroup.blocksRaycasts = false;
        item.transform.SetParent(draggablesTransform);
        item.transform.SetAsLastSibling();
    }

    public void EquipEquipment(SlotTag tag, InventoryItem item = null)
    {
        switch (tag)
        {
            case SlotTag.Head:
                if (item == null)
                {
                    Debug.Log("Unequipped helmet on " + tag);
                }
                else
                {
                    Debug.Log("Equipped " + item.myItem.name + " on " + tag);
                }
                break;
            case SlotTag.Chest:
                break;
            case SlotTag.Legs:
                break;
            case SlotTag.Feet:
                break;
        }
    }

    public void AddItem(Item item)
    {
        Debug.Log("Adding item to inventory: " + item.name);

        foreach (var slot in inventorySlots)
        {
            if (slot.myItem != null && slot.myItem.myItem == item)
            {
                if (slot.myItem.quantity < item.maxStack)
                {
                    int spaceLeft = item.maxStack - slot.myItem.quantity;
                    if (spaceLeft > 0)
                    {
                        int quantityToAdd = Mathf.Min(spaceLeft, 1);
                        slot.myItem.AddQuantity(quantityToAdd);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].myItem == null)
            {
                InventoryItem newItem = Instantiate(itemPrefab, inventorySlots[i].transform);
                newItem.Initialize(item, inventorySlots[i]);
                Debug.Log("Item added to inventory slot: " + i);
                break;
            }
        }
    }

    public void SplitStack(InventoryItem originalItem)
    {
        originalItem.AddQuantity(-1);

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].myItem == null)
            {
                InventoryItem newItem = Instantiate(itemPrefab, inventorySlots[i].transform);
                newItem.Initialize(originalItem.myItem, inventorySlots[i]);
                newItem.quantity = 1;
                newItem.UpdateQuantityText();
                newItem.transform.SetParent(draggablesTransform);
                newItem.transform.SetAsLastSibling();
                SetCarriedItem(newItem);
                break;
            }
        }
    }

    public void MergeStack(InventoryItem itemToMerge)
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.myItem != null && slot.myItem.myItem == itemToMerge.myItem && slot.myItem != itemToMerge)
            {
                int spaceLeft = itemToMerge.myItem.maxStack - slot.myItem.quantity;
                if (spaceLeft > 0)
                {
                    int quantityToAdd = Mathf.Min(spaceLeft, itemToMerge.quantity);
                    slot.myItem.AddQuantity(quantityToAdd);
                    itemToMerge.AddQuantity(-quantityToAdd);

                    if (itemToMerge.quantity <= 0)
                    {
                        Destroy(itemToMerge.gameObject);
                    }
                    return;
                }
            }
        }
    }

    public void SpawnRandomInventoryItem()
    {
        Item randomItem = PickRandomItem();
        AddItem(randomItem);
    }

    Item PickRandomItem()
    {
        int random = Random.Range(0, items.Length);
        return items[random];
    }
}