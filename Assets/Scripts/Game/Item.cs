using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
<<<<<<< HEAD
=======
    public Sprite itemIcon;
    public SlotTag itemTag;

    [Header("If the item can be equipped")]
    public GameObject equipmentPrefab;
>>>>>>> parent of c649bda (Merge pull request #17 from O-WigglyPlay/Fild)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
<<<<<<< HEAD
            Destroy(gameObject); // 아이템이 플레이어와 충돌하면 아이템을 파괴합니다.
=======
            Destroy(gameObject);
>>>>>>> parent of c649bda (Merge pull request #17 from O-WigglyPlay/Fild)
        }
    }
}
