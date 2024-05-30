using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparent : MonoBehaviour
{
    public Collider2D buildingCollider; // 건축물의 BoxCollider2D
    public Collider2D transparentTrigger; // 건축물 뒤의 투명화 트리거 영역

    private SpriteRenderer spriteRenderer; // 건축물의 SpriteRenderer

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 투명화 처리
            Color color = spriteRenderer.color;
            color.a = 0.5f; // 투명도 설정 (0.0 ~ 1.0)
            spriteRenderer.color = color;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 원래 상태로 복구
            Color color = spriteRenderer.color;
            color.a = 1.0f;
            spriteRenderer.color = color;
        }
    }
}