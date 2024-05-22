using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
<<<<<<< HEAD
=======
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> parent of 2086bf1 (3ì¸ë¶„ í†µí•©í•˜ê¸°)
    public Sprite itemIcon;
    public SlotTag itemTag;

    [Header("If the item can be equipped")]
    public GameObject equipmentPrefab;
<<<<<<< HEAD
=======
>>>>>>> parent of c649bda (Merge pull request #17 from O-WigglyPlay/Fild)
=======
>>>>>>> parent of efb321a (Reapply "ì¸ë²¤í† ë¦¬ ì„±ê³µ!")
>>>>>>> parent of 2086bf1 (3ì¸ë¶„ í†µí•©í•˜ê¸°)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
<<<<<<< HEAD
            Destroy(gameObject);
=======
<<<<<<< HEAD
<<<<<<< HEAD
            Destroy(gameObject); // ¾ÆÀÌÅÛÀÌ ÇÃ·¹ÀÌ¾î¿Í Ãæµ¹ÇÏ¸é ¾ÆÀÌÅÛÀ» ÆÄ±«ÇÕ´Ï´Ù.
=======
            Destroy(gameObject);
>>>>>>> parent of c649bda (Merge pull request #17 from O-WigglyPlay/Fild)
=======
            Destroy(gameObject); // ¾ÆÀÌÅÛÀÌ ÇÃ·¹ÀÌ¾î¿Í Ãæµ¹ÇÏ¸é ¾ÆÀÌÅÛÀ» ÆÄ±«ÇÕ´Ï´Ù.
>>>>>>> parent of efb321a (Reapply "ì¸ë²¤í† ë¦¬ ì„±ê³µ!")
>>>>>>> parent of 2086bf1 (3ì¸ë¶„ í†µí•©í•˜ê¸°)
        }
    }
}
