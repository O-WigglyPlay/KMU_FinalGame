using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotTag { None, Head, Chest, Legs, Feet }

public class Item : MonoBehaviour
{
    public Sprite itemIcon;
    public SlotTag itemTag;
    public int maxStack = 10; // 스택 가능한 최대 개수를 60으로 설정

    [Header("If the item can be equipped")]
    public GameObject equipmentPrefab;

    [Header("Combination Settings")]
    public CombineRecipe[] combineRecipes; // 조합 레시피 배열

    private bool isPlayerInRange = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            AddToInventory();
        }
    }

    private void AddToInventory()
    {
        // 인벤토리에 아이템 추가
        Inventory.Singleton.AddItem(this);
        Destroy(gameObject);
    }
}