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
            Debug.Log("F key pressed."); // F Ű �Է� Ȯ��

            // Inventory.Singleton�� null���� Ȯ���մϴ�.
            if (Inventory.Singleton == null)
            {
                Debug.LogError("Inventory.Singleton is null.");
                return;
            }

            // craftBox ������ �Ҵ�Ǿ����� Ȯ���մϴ�.
            if (craftBox == null)
            {
                Debug.LogError("craftBox is not assigned.");
                return;
            }

            // CraftBox �������� �κ��丮�� �ִ��� Ȯ���մϴ�.
            if (Inventory.Singleton.HasItem(craftBox))
            {
                Debug.Log("CraftBox is in the inventory.");
                // �г��� Ȱ��ȭ ���¸� ��ȯ�մϴ�.
                craftingPanel.SetActive(!craftingPanel.activeSelf);
            }
            else
            {
                Debug.Log("CraftBox is not in the inventory.");
            }
        }
    }
}
