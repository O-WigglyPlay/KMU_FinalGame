using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    private Image itemIcon;
    public CanvasGroup canvasGroup { get; private set; }
    public TextMeshProUGUI quantityText; // TextMeshPro를 사용하여 아이템 개수를 표시할 텍스트

    public Item myItem { get; set; }
    public InventorySlot activeSlot { get; set; }
    public int quantity = 1; // 아이템 개수 추가

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        itemIcon = GetComponent<Image>();
        quantityText = GetComponentInChildren<TextMeshProUGUI>();
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
        }

        UpdateQuantityText(); // 초기 아이템 개수를 텍스트로 표시
    }

    public void AddQuantity(int amount)
    {
        quantity += amount;
        if (quantity <= 0)
        {
            Destroy(gameObject); // 아이템 수량이 0 이하일 경우 게임 오브젝트 파괴
        }
        UpdateQuantityText(); // 아이템 개수가 변경될 때마다 텍스트 업데이트
    }

    // 아이템 개수를 표시하는 텍스트를 업데이트하는 메서드
    public void UpdateQuantityText()
    {
        if (quantityText != null)
        {
            quantityText.text = quantity.ToString();
            quantityText.gameObject.SetActive(quantity > 1); // 개수가 1보다 큰 경우에만 텍스트를 표시
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
}
