using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer itemImage;
    [SerializeField] private TextMeshPro itemCountText;
    [SerializeField] private Item targetItem;
    [SerializeField] private int removeQuantity = 10; // 한 번에 제거할 아이템 수량
    [SerializeField] private int targetQuantity = 1000; // 목표 총량

    private int accumulatedCount = 0; // 누적된 아이템 개수를 추적하는 변수

    private void Start()
    {
        itemImage.sprite = targetItem.itemIcon; // 아이템 이미지를 설정합니다.
        UpdateItemCountText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TryRemoveItem();
        }
    }

    private void TryRemoveItem()
    {
        int currentItemCount = GetCurrentItemCount();

        if (currentItemCount >= removeQuantity)
        {
            RemoveItemFromInventory();
            accumulatedCount += removeQuantity; // 누적된 아이템 개수를 업데이트합니다.
            UpdateItemCountText();
            CheckIfTargetReached();
        }
    }

    private int GetCurrentItemCount()
    {
        int itemCount = 0;

        foreach (var slot in Inventory.Singleton.GetInventorySlots())
        {
            if (slot.myItem != null && slot.myItem.myItem == targetItem)
            {
                itemCount += slot.myItem.quantity;
            }
        }

        return itemCount;
    }

    private void RemoveItemFromInventory()
    {
        int quantityToRemove = removeQuantity;

        foreach (var slot in Inventory.Singleton.GetInventorySlots())
        {
            if (slot.myItem != null && slot.myItem.myItem == targetItem)
            {
                int slotQuantity = slot.myItem.quantity;

                if (slotQuantity >= quantityToRemove)
                {
                    slot.myItem.AddQuantity(-quantityToRemove);
                    if (slot.myItem.quantity == 0)
                    {
                        Destroy(slot.myItem.gameObject);
                        slot.myItem = null;
                    }
                    break;
                }
                else
                {
                    slot.myItem.AddQuantity(-slotQuantity);
                    Destroy(slot.myItem.gameObject);
                    slot.myItem = null;
                    quantityToRemove -= slotQuantity;
                }
            }
        }
    }

    private void UpdateItemCountText()
    {
        itemCountText.text = $"{accumulatedCount} / {targetQuantity}";
    }

    private void CheckIfTargetReached()
    {
        if (accumulatedCount >= targetQuantity)
        {
            Destroy(gameObject); // 게임 오브젝트 삭제
        }
    }
}
