using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
<<<<<<< HEAD
<<<<<<< HEAD
=======
    public Sprite itemIcon;
    public SlotTag itemTag;

    [Header("If the item can be equipped")]
    public GameObject equipmentPrefab;
>>>>>>> parent of c649bda (Merge pull request #17 from O-WigglyPlay/Fild)
=======
>>>>>>> parent of efb321a (Reapply "인벤토리 성공!")
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
<<<<<<< HEAD
<<<<<<< HEAD
            Destroy(gameObject); // �������� �÷��̾�� �浹�ϸ� �������� �ı��մϴ�.
=======
            Destroy(gameObject);
>>>>>>> parent of c649bda (Merge pull request #17 from O-WigglyPlay/Fild)
=======
            Destroy(gameObject); // �������� �÷��̾�� �浹�ϸ� �������� �ı��մϴ�.
>>>>>>> parent of efb321a (Reapply "인벤토리 성공!")
        }
    }
}
