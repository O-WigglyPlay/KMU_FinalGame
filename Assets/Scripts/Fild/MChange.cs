using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MChange : MonoBehaviour
{
    public Sprite[] destructionSprites; // 파괴 스프라이트들의 배열
    public float destructionDelay = 1.0f; // 파괴 지연 시간

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
            // 다음 파괴 스프라이트로 변경
            spriteRenderer.sprite = destructionSprites[destructionStage];
            destructionStage++;
        }
        else
        {
            // 마지막 파괴 스프라이트일 때 게임 오브젝트 파괴
            Destroy(gameObject, destructionDelay);
            isDestroyed = true;
        }
    }

}
