using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    public InventoryItem myItem { get; set; }
    public SlotTag myTag;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Inventory.carriedItem == null) return;
            if (myTag != SlotTag.None && Inventory.carriedItem.myItem.itemTag != myTag) return;
            SetItem(Inventory.carriedItem);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (myItem != null && myItem.quantity > 1)
            {
                Inventory.Singleton.SplitStack(myItem);
            }
        }
    }

    public void SetItem(InventoryItem item)
    {
        if (myItem != null && myItem.myItem == item.myItem)
        {
            // Combine stacks
            int spaceLeft = myItem.myItem.maxStack - myItem.quantity;
            if (spaceLeft > 0)
            {
                int quantityToAdd = Mathf.Min(spaceLeft, item.quantity);
                myItem.AddQuantity(quantityToAdd);
                item.AddQuantity(-quantityToAdd);

                if (item.quantity <= 0)
                {
                    Destroy(item.gameObject);
                }
                Inventory.carriedItem = null; // 드래그 상태 해제
                return;
            }
        }
        else
        {
            // Replace item
            Inventory.carriedItem = null;

            if (item.activeSlot != null)
            {
                item.activeSlot.myItem = null;
            }

            myItem = item;
            myItem.activeSlot = this;
            myItem.transform.SetParent(transform);
            myItem.transform.localPosition = Vector3.zero;
            myItem.canvasGroup.blocksRaycasts = true;

            if (myTag != SlotTag.None)
            {
                Inventory.Singleton.EquipEquipment(myTag, myItem);
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem droppedItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (droppedItem != null)
        {
            SetItem(droppedItem);
        }
    }
}