using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MChange : MonoBehaviour
{
    public Sprite[] destructionSprites; // �ı� ��������Ʈ���� �迭
    public float destructionDelay = 1.0f; // �ı� ���� �ð�

    private SpriteRenderer spriteRenderer;
    private int destructionStage = 0;
    private bool isDestroyed = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDestroyed)
        {
            DestroyRock();
        }
    }

    void DestroyRock()
    {
        if (destructionStage < destructionSprites.Length - 1)
        {
            // ���� �ı� ��������Ʈ�� ����
            spriteRenderer.sprite = destructionSprites[destructionStage];
            destructionStage++;
        }
        else
        {
            // ������ �ı� ��������Ʈ�� �� ���� ������Ʈ �ı�
            Destroy(gameObject, destructionDelay);
            isDestroyed = true;
        }
    }

}
