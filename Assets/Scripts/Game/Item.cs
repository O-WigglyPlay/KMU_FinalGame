using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Item : MonoBehaviour
{
    public Sprite itemIcon;
    public SlotTag itemTag;
    public int maxStack = 60; // 스택 가능한 최대 개수를 60으로 설정

    [Header("If the item can be equipped")]
    public GameObject equipmentPrefab;

    private bool isPlayerInRange = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Player entered the range of the item.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Player exited the range of the item.");
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
        if (Inventory.Singleton == null)
        {
            Debug.LogError("Inventory.Singleton이 null입니다. Inventory 스크립트가 제대로 초기화되지 않았습니다.");
            return;
        }

        // 인벤토리에 아이템 추가
        Debug.Log("Adding item to inventory: " + name);
        Inventory.Singleton.AddItem(this);
        Destroy(gameObject);
    }
}