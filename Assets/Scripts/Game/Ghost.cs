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
    private Rigidbody2D rb; // Rigidbody2D 컴포넌트
    private SpriteRenderer spriteRenderer; // SpriteRenderer 컴포넌트

    private void Start()
    {
        animator = GetComponent<Animator>(); // 애니메이터 컴포넌트 가져오기
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer 컴포넌트 가져오기

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
        rb.velocity = directionToPlayer * moveSpeed;

        // 유령이 플레이어를 바라보도록 스프라이트 뒤집기 (반대 방향)
        if (directionToPlayer.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        // 플레이어와의 거리를 계산
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // 플레이어가 몬스터의 근접 공격 범위 내에 있고, 공격 쿨다운이 지났을 때 공격 시작
        if (distanceToPlayer <= attackRange && Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;

            // 애니메이션 시작
            animator.SetTrigger("Attack");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 대상이 플레이어인지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어의 Rigidbody2D를 Kinematic으로 설정하여 물리적 충돌 반응을 제거
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.bodyType = RigidbodyType2D.Kinematic;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // 충돌한 대상이 플레이어인지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            // 공격 쿨다운 시간이 지났다면 체력 감소
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                lastAttackTime = Time.time;

                // 플레이어의 체력을 감소시키는 로직
                Player player = collision.gameObject.GetComponent<Player>();
                if (player != null)
                {
                    player.n_Hp -= attackDamage;

                    // 플레이어 체력이 0 이하인지 확인하여 사망 처리
                    if (player.n_Hp <= 0)
                    {
                        player.Die();
                    }
                }

                // 애니메이션 시작
                animator.SetTrigger("Attack");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 충돌한 대상이 플레이어인지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어의 Rigidbody2D를 다시 Dynamic으로 설정하여 원래 상태로 복귀
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어의 공격과 충돌했는지 확인
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            Debug.Log("플레이어의 공격과 충돌");
            // 유령의 체력을 감소시키는 로직
            TakeDamage(Player.instance.n_Dmg);
        }
    }

    private void TakeDamage(int damage)
    {
        curHealth -= damage;
        Debug.Log("유령 체력 감소: " + curHealth);

        // 유령 체력이 0 이하인지 확인하여 사망 처리
        if (curHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // 유령 사망 처리 로직
        animator.SetTrigger("Die");
        rb.velocity = Vector2.zero;
        // 일정 시간 후 유령 오브젝트 비활성화
        Invoke("DeactivateGhost", 1f);
    }

    private void DeactivateGhost()
    {
        gameObject.SetActive(false);
    }
}