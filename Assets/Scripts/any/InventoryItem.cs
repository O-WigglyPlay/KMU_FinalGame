using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro; // TextMeshPro 네임스페이스 추가

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    private Image itemIcon;
    public CanvasGroup canvasGroup { get; private set; }
    public TextMeshPro quantityText; // TextMeshPro를 사용하여 아이템 개수를 표시할 텍스트

    public Item myItem { get; set; }
    public InventorySlot activeSlot { get; set; }
    public int quantity = 1; // 아이템 개수 추가

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        itemIcon = GetComponent<Image>();

        if (itemIcon == null)
        {
            Debug.LogError("itemIcon is not assigned in InventoryItem.");
        }

        // TextMeshProUGUI 컴포넌트 참조
        quantityText = GetComponentInChildren<TextMeshPro>();
        if (quantityText == null)
        {
            Debug.LogError("quantityText is not assigned in InventoryItem.");
        }

        UpdateQuantityText(); // 초기 아이템 개수를 텍스트로 표시
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

        UpdateQuantityText(); // 초기 아이템 개수를 텍스트로 표시
    }

    public void AddQuantity(int amount)
    {
        quantity += amount;
        Debug.Log(myItem.name + " quantity: " + quantity);
        UpdateQuantityText(); // 아이템 개수가 변경될 때마다 텍스트 업데이트
    }

    // 아이템 개수를 표시하는 텍스트를 업데이트하는 메서드
    private void UpdateQuantityText()
    {
        if (quantityText != null)
        {
            quantityText.text = quantity.ToString();
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
