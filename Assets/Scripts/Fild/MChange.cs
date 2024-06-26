using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MChange : MonoBehaviour
{
    public int Mineral_Hp = 10;
    public Sprite[] destructionSprites; // 파괴 스프라이트들의 배열
    public float destructionDelay = 1.0f; // 파괴 지연 시간
    public GameObject mineralPrefab;  // Mineral 프리팹에 대한 참조

    private SpriteRenderer spriteRenderer;
    private int maxHp; // 최대 체력
    private bool isDestroyed = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        maxHp = Mineral_Hp; // 최대 체력을 저장
    }

    void UpdateSprite()
    {
        // 체력에 비례하여 스프라이트 변경
        int spriteIndex = Mathf.FloorToInt((1 - (float)Mineral_Hp / maxHp) * destructionSprites.Length);
        spriteIndex = Mathf.Clamp(spriteIndex, 0, destructionSprites.Length - 1);
        spriteRenderer.sprite = destructionSprites[spriteIndex];
    }

    public void TakeDamage(int damage)
    {
        if (isDestroyed) return;

        Mineral_Hp -= damage;
        UpdateSprite();

        if (Mineral_Hp <= 0)
        {
            StartCoroutine(DestroyRock());
        }
    }

    private IEnumerator DestroyRock()
    {
        if (!isDestroyed)
        {
            isDestroyed = true;

            // 마지막 파괴 스프라이트로 변경
            spriteRenderer.sprite = destructionSprites[destructionSprites.Length - 1];

            // 지연 시간 후에 프리팹 생성 및 오브젝트 파괴
            yield return new WaitForSeconds(destructionDelay);

            if (mineralPrefab != null)
            {
                Instantiate(mineralPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
