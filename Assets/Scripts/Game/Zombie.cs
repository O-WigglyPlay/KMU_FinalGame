using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float moveSpeed; // 몬스터의 이동 속도
    public float attackRange = 2f; // 몬스터의 근접 공격 범위
    public float attackCooldown = 1f; // 공격 쿨다운 시간
    public int attackDamage = 10; // 공격 데미지

    public GameObject ZombieArm;   //팔 프리펩
    public GameObject ZombieBody;   //몸통 아이템
    public GameObject ZombieHead;  //머리 아이템
    public GameObject ZombieFeet;  //발 아이템

    public float armDropChance = 0.25f; // 팔 아이템 드랍 확률
    public float bodyDropChance = 0.25f; // 몸통 아이템 드랍 확률
    public float headDropChance = 0.25f; // 머리 아이템 드랍 확률
    public float legDropChance = 0.25f; // 다리 아이템 드랍 확률

    bool isLive = true;
    bool isAttacking = false;
    float lastAttackTime;

    Rigidbody2D rigid;
    SpriteRenderer spriter;

    private Animator animator; // 몬스터의 애니메이터
    private Transform playerTransform; // 플레이어의 Transform 컴포넌트

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.bodyType = RigidbodyType2D.Kinematic; // Rigidbody2D를 Kinematic으로 설정
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

        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(rigid.position, playerTransform.position);

        if (distanceToPlayer > attackRange)
        {
            Vector2 nextPosition = Vector2.MoveTowards(rigid.position, playerTransform.position, moveSpeed * Time.fixedDeltaTime);
            rigid.MovePosition(nextPosition);
        }

        // 몬스터가 플레이어를 보고 있는지 확인하고 방향을 설정
        spriter.flipX = playerTransform.position.x < rigid.position.x;

        // 플레이어와의 거리가 근접 공격 범위 이내이고 공격 쿨다운이 지났으면 공격
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            StartCoroutine(Attack());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어와 충돌했을 때 처리 로직 추가
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                // 플레이어에게 데미지를 줍니다.
                player.TakeDamage(attackDamage);
            }
        }
    }

    public void Die()
    {
        if (isLive)
        {
            isLive = false; // 생존 상태를 false로 변경

            // 아이템 드랍 확률을 체크하여 아이템을 생성할지 결정
            if (Random.value <= armDropChance)
            {
                // 팔 아이템 생성
                Instantiate(ZombieArm, transform.position, Quaternion.identity);
            }
            if (Random.value <= bodyDropChance)
            {
                // 몸통 아이템 생성
                Instantiate(ZombieBody, transform.position, Quaternion.identity);
            }
            if (Random.value <= headDropChance)
            {
                // 머리 아이템 생성
                Instantiate(ZombieHead, transform.position, Quaternion.identity);
            }
            if (Random.value <= legDropChance)
            {
                // 다리 아이템 생성
                Instantiate(ZombieFeet, transform.position, Quaternion.identity);
            }

            // 적 오브젝트를 파괴
            Destroy(gameObject);
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true; // 공격 중 상태로 변경
        animator.SetBool("ZombieAttack", isAttacking); // 공격 애니메이션 재생

        float attackAnimationLength = 1.0f; // 공격 애니메이션의 실제 길이로 변경
        // 공격 모션이 끝날 때까지 대기
        yield return new WaitForSeconds(attackAnimationLength);

        // 공격 후 쿨다운 갱신
        lastAttackTime = Time.time;
        isAttacking = false; // 공격 종료 상태로 변경
        animator.SetBool("ZombieAttack", isAttacking); // 공격 애니메이션 재생
    }
}
