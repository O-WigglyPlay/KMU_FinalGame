using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float attackRange = 2f; // 몬스터의 근접 공격 범위
    public float attackCooldown = 1f; // 공격 쿨다운 시간
    public int attackDamage = 10; // 공격 데미지

    private Animator animator; // 몬스터의 애니메이터
    private bool isAttacking = false; // 공격 중인지 여부
    private float lastAttackTime = 0f; // 마지막 공격 시간

    public GameObject player; //플레이어 오브젝트 
    private Transform playerTransform; // 플레이어의 Transform 컴포넌트

    
    private void Start()
    {
        animator = GetComponent<Animator>(); // 애니메이터 컴포넌트 가져오기

        // 플레이어 오브젝트를 참조하여 플레이어의 Transform을 가져옴
        playerTransform = player != null ? player.transform : null;
    }

    private void Update()
    {
        if (!isAttacking)
        {
            // 플레이어를 향하는 방향 벡터 계산
            Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;

            // 몬스터의 회전을 플레이어를 향하도록 설정
            transform.LookAt(playerTransform);

            // 플레이어와의 거리를 계산
            float distanceToPlayer = Vector3.Distance(transform.position, Player.instance.transform.position);

            // 플레이어가 몬스터의 근접 공격 범위 내에 있고, 공격 쿨다운이 지났을 때 공격 시작
            if (distanceToPlayer <= attackRange && Time.time - lastAttackTime >= attackCooldown)
            {
                isAttacking = true;
                lastAttackTime = Time.time;

                // 애니메이션 시작
                animator.SetTrigger("Attack");
            }
        }
    }

    // Animation Event: Attack Event
    public void PerformAttack()
    {
        // 플레이어에게 데미지를 입힘 (플레이어가 몬스터의 자식 객체라고 가정)
        Player player = Player.instance;
        if (player != null)
        {
            player.TakeDamage(attackDamage);
        }
    }

    // Animation Event: End Attack
    public void EndAttack()
    {
        isAttacking = false;
    }
}