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

    public InventorySlot[] GetInventorySlots()
    {
        return inventorySlots;
    }

    // 0=Head, 1=Chest, 2=Legs, 3=Feet
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
    }

    public void EquipEquipment(SlotTag tag, InventoryItem item = null)
    {
        switch (tag)
        {
            case SlotTag.Head:
                if (item == null)
                {
                    // Destroy item.equipmentPrefab on the Player Object;
                    Debug.Log("Unequipped helmet on " + tag);
                }
                else
                {
                    // Instantiate item.equipmentPrefab on the Player Object;
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

        // 기존에 동일한 아이템이 있는지 확인
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

        // 새로운 슬롯에 아이템 추가
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
        // 스택된 아이템 중 하나를 분리하여 새로운 아이템으로 만듭니다.
        originalItem.AddQuantity(-1);

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].myItem == null)
            {
                InventoryItem newItem = Instantiate(itemPrefab, inventorySlots[i].transform);
                newItem.Initialize(originalItem.myItem, inventorySlots[i]);
                newItem.quantity = 1;
                newItem.UpdateQuantityText();
                SetCarriedItem(newItem); // 분리된 아이템을 드래그 상태로 설정
                break;
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