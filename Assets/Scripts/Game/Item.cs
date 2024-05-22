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
>>>>>>> parent of efb321a (Reapply "Ïù∏Î≤§ÌÜ†Î¶¨ ÏÑ±Í≥µ!")
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
<<<<<<< HEAD
<<<<<<< HEAD
            Destroy(gameObject); // æ∆¿Ã≈€¿Ã «√∑π¿ÃæÓøÕ √Êµπ«œ∏È æ∆¿Ã≈€¿ª ∆ƒ±´«’¥œ¥Ÿ.
=======
            Destroy(gameObject);
>>>>>>> parent of c649bda (Merge pull request #17 from O-WigglyPlay/Fild)
=======
            Destroy(gameObject); // æ∆¿Ã≈€¿Ã «√∑π¿ÃæÓøÕ √Êµπ«œ∏È æ∆¿Ã≈€¿ª ∆ƒ±´«’¥œ¥Ÿ.
>>>>>>> parent of efb321a (Reapply "Ïù∏Î≤§ÌÜ†Î¶¨ ÏÑ±Í≥µ!")
        }
    }
}
