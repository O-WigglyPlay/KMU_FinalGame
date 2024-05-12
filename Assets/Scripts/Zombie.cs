using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float moveSpeed; // 몬스터의 이동 속도
    public float attackRange = 2f; // 몬스터의 근접 공격 범위
    public float attackCooldown = 1f; // 공격 쿨다운 시간
    public float attackDamage = 10f; // 공격 데미지

    public Rigidbody2D target;

    bool isLive = true;

    Rigidbody2D rigid;
    SpriteRenderer spriter;

    private Animator animator; // 몬스터의 애니메이터
    private Transform playerTransform; // 플레이어의 Transform 컴포넌트

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // 애니메이터 컴포넌트 가져오기
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isLive)
        {
            return;
        }
        Vector2 dirvec = target.position - rigid.position;
        Vector2 nextVec = dirvec.normalized * moveSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!isLive)
        {
            return;
        }

        spriter.flipX = target.position.x < rigid.position.y;
    }
}
