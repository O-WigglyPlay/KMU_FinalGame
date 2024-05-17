using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitCtrl : MonoBehaviour
{
    public float speed = 5f; // 투사체의 속도
    private Vector2 targetDirection;

    // 투사체의 이동 방향 설정
    public void SetDirection(Vector2 direction)
    {
        targetDirection = direction.normalized;
    }

    void Update()
    {
        // 투사체 이동
        transform.Translate(targetDirection * speed * Time.deltaTime);

        // 일정 시간이 지나면 투사체를 파괴 (필요에 따라 조정 가능)
        Destroy(gameObject, 5f);
    }

    // 충돌 처리 (필요에 따라 수정 가능)
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 여기서 충돌 로직 추가
        // 예: 플레이어와 충돌 시 데미지 처리 등
        Destroy(gameObject); // 충돌 시 투사체 파괴
    }
}
