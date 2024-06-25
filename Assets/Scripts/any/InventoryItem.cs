using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    private Image itemIcon;
    public CanvasGroup canvasGroup { get; private set; }
    public TextMeshProUGUI quantityText;

    public Item myItem { get; set; }
    public InventorySlot activeSlot { get; set; }
    public int quantity = 1;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        itemIcon = GetComponent<Image>();

        if (itemIcon == null)
        {
            Debug.LogError("itemIcon is not assigned in InventoryItem.");
        }

        quantityText = GetComponentInChildren<TextMeshProUGUI>();
        if (quantityText == null)
        {
            Debug.LogError("quantityText is not assigned in InventoryItem.");
        }

        UpdateQuantityText();
    }

    public void Initialize(Item item, InventorySlot parent)
    {
        myItem = item;
        activeSlot = parent;
        activeSlot.myItem = this;

        itemIcon.sprite = item.itemIcon;
        UpdateQuantityText();
    }

    public void AddQuantity(int amount)
    {
        quantity += amount;
        UpdateQuantityText();
    }

    public void UpdateQuantityText()
    {
        if (quantityText != null)
        {
            quantityText.text = quantity.ToString();
            quantityText.gameObject.SetActive(quantity > 1);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Inventory.Singleton.SetCarriedItem(this);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (quantity > 1)
            {
                Inventory.Singleton.SplitStack(this);
            }
        }
    }

    private void OnDestroy()
    {
        if (activeSlot != null && activeSlot.myItem == this)
        {
            activeSlot.myItem = null;
        }
    }
}