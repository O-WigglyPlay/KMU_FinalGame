using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftHandler : MonoBehaviour
{
    [SerializeField] private GameObject craftingPanel; // 패널 오브젝트
    [SerializeField] private Item craftBox; // CraftBox 아이템

    void Start()
    {
        // 처음에는 패널을 비활성화 상태로 설정합니다.
        craftingPanel.SetActive(false);
    }

    void Update()
    {
        // F 키를 눌렀을 때의 동작을 처리합니다.
        if (Input.GetKeyDown(KeyCode.F))
        {
            // CraftBox 아이템이 인벤토리에 있는지 확인합니다.
            if (Inventory.Singleton.HasItem(craftBox))
            {
                // 패널의 활성화 상태를 전환합니다.
                craftingPanel.SetActive(!craftingPanel.activeSelf);
            }
        }
    }
}
