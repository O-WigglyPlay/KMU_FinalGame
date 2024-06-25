using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    private Image itemIcon;
    public CanvasGroup canvasGroup { get; private set; }

    public Item myItem { get; set; }
    public InventorySlot activeSlot { get; set; }

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        itemIcon = GetComponent<Image>();

        if (itemIcon == null)
        {
            Debug.LogError("itemIcon is not assigned in InventoryItem.");
        }
    }

    public void Initialize(Item item, InventorySlot parent)
    {
        myItem = item;
        activeSlot = parent;
        activeSlot.myItem = this;

        // 아이템 아이콘을 인벤토리 슬롯에 설정
        itemIcon = GetComponent<Image>();
        if (itemIcon != null)
        {
            itemIcon.sprite = item.itemIcon;
            if (itemIcon.sprite == null)
            {
                Debug.LogError("itemIcon.sprite is null. Check if itemIcon is properly set in Item asset.");
            }
        }
        else
        {
            Debug.LogError("itemIcon is not assigned in Initialize.");
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            Inventory.Singleton.SetCarriedItem(this);
        }
    }
}