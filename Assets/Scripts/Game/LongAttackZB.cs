using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class LongAttackZB : MonoBehaviour
{
    public float moveSpeed; // 몬스터의 이동 속도
    public float attackRange = 5f; // 원거리 공격 범위
    public float attackCooldown = 3f; // 공격 쿨다운 시간
    public GameObject ZBSpitPrefab; // 투사체 프리펩

    Rigidbody2D rigid;
    SpriteRenderer spriter;

    bool isLive = true;
    bool isAttacking = false;
    float lastAttackTime;

    private Animator animator; // 몬스터의 애니메이터
    private Transform playerTransform; // 플레이어의 Transform 컴포넌트

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true; // 회전을 고정
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // 애니메이터 컴포넌트 가져오기
    }

    private void Start()
    {
        // 플레이어를 찾아서 설정
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void FixedUpdate()
    {
        if (!isLive || isAttacking)
        {
            return;
        }
        Vector2 dirvec = (Vector2)(playerTransform.position - transform.position);
        Vector2 nextVec = dirvec.normalized * moveSpeed * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;

        // 몬스터가 플레이어를 보고 있는지 확인하고 방향을 설정
        spriter.flipX = playerTransform.position.x > rigid.position.x;

        // 플레이어와의 거리 계산
        float distanceToPlayer = Vector2.Distance(rigid.position, playerTransform.position);

        // 플레이어와의 거리가 원거리 공격 범위 이내이고 공격 쿨다운이 지났으면 공격
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            StartCoroutine(RangedAttack());
        }
    }

    IEnumerator RangedAttack()
    {
        isAttacking = true;
        animator.SetBool("ZombieLongA", isAttacking); // 원거리 공격 애니메이션 재생

        float attackAnimationLength = 1.0f; // 원거리 공격 애니메이션 길이
        yield return new WaitForSeconds(attackAnimationLength / 2); // 애니메이션 중간에 투사체 발사

        Vector2 direction = (Vector2)(playerTransform.position - transform.position).normalized;
        GameObject ZBSpit = Instantiate(ZBSpitPrefab, transform.position, Quaternion.identity);
        ZBSpit.GetComponent<SpitCtrl>().SetDirection(direction); // 투사체의 방향 설정

        lastAttackTime = Time.time;
        yield return new WaitForSeconds(attackAnimationLength / 2);

        isAttacking = false;
        animator.SetBool("ZombieLongA", isAttacking);
    }

}