using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftHandler : MonoBehaviour
{
    [SerializeField] private GameObject craftingPanel; // �г� ������Ʈ
    [SerializeField] private Item craftBox; // CraftBox ������

    void Start()
    {
        // ó������ �г��� ��Ȱ��ȭ ���·� �����մϴ�.
        craftingPanel.SetActive(false);
    }

    void Update()
    {
        // F Ű�� ������ ���� ������ ó���մϴ�.
        if (Input.GetKeyDown(KeyCode.F))
        {
            // CraftBox �������� �κ��丮�� �ִ��� Ȯ���մϴ�.
            if (Inventory.Singleton.HasItem(craftBox))
            {
                // �г��� Ȱ��ȭ ���¸� ��ȯ�մϴ�.
                craftingPanel.SetActive(!craftingPanel.activeSelf);
            }
        }
    }
}
