using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public int maxHealth;  //최대 체력
    public int curHealth;  //현재 체력

    public float moveSpeed; // 몬스터의 이동 속도
    public float attackRange; // 몬스터의 근접 공격 범위
    public float attackCooldown; // 공격 쿨다운 시간
    public int attackDamage; // 공격 데미지

    private Animator animator; // 몬스터의 애니메이터
    private float lastAttackTime = 0f; // 마지막 공격 시간
    private Transform playerTransform; // 플레이어의 Transform 컴포넌트

    private void Start()
    {
        animator = GetComponent<Animator>(); // 애니메이터 컴포넌트 가져오기

        // 플레이어 오브젝트를 참조하여 플레이어의 Transform을 가져옴
        playerTransform = Player.instance.transform;

        // 초기 체력 설정
        curHealth = maxHealth;
    }

    private void Update()
    {
        // 플레이어를 향하는 방향 벡터 계산
        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;

        // 플레이어 방향으로 몬스터 이동
        transform.Translate(directionToPlayer * moveSpeed * Time.deltaTime);

        // 플레이어와의 거리를 계산
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // 플레이어가 몬스터의 근접 공격 범위 내에 있고, 공격 쿨다운이 지났을 때 공격 시작
        if (distanceToPlayer <= attackRange && Time.time - lastAttackTime >= attackCooldown)
        {
            //isAttacking = true;
            lastAttackTime = Time.time;

            // 애니메이션 시작
            animator.SetTrigger("Attack");
        }
    }
}