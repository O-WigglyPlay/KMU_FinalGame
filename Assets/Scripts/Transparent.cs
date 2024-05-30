using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparent : MonoBehaviour
{
    public Collider2D buildingCollider; // ���๰�� BoxCollider2D
    public Collider2D transparentTrigger; // ���๰ ���� ����ȭ Ʈ���� ����

    private SpriteRenderer spriteRenderer; // ���๰�� SpriteRenderer

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ����ȭ ó��
            Color color = spriteRenderer.color;
            color.a = 0.5f; // ���� ���� (0.0 ~ 1.0)
            spriteRenderer.color = color;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ���� ���·� ����
            Color color = spriteRenderer.color;
            color.a = 1.0f;
            spriteRenderer.color = color;
        }
    }
}