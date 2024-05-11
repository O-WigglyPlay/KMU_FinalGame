using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float attackRange = 2f; // ������ ���� ���� ����
    public float attackCooldown = 1f; // ���� ��ٿ� �ð�
    public int attackDamage = 10; // ���� ������

    private Animator animator; // ������ �ִϸ�����
    private bool isAttacking = false; // ���� ������ ����
    private float lastAttackTime = 0f; // ������ ���� �ð�

    public GameObject player; //�÷��̾� ������Ʈ 
    private Transform playerTransform; // �÷��̾��� Transform ������Ʈ

    
    private void Start()
    {
        animator = GetComponent<Animator>(); // �ִϸ����� ������Ʈ ��������

        // �÷��̾� ������Ʈ�� �����Ͽ� �÷��̾��� Transform�� ������
        playerTransform = player != null ? player.transform : null;
    }

    private void Update()
    {
        if (!isAttacking)
        {
            // �÷��̾ ���ϴ� ���� ���� ���
            Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;

            // ������ ȸ���� �÷��̾ ���ϵ��� ����
            transform.LookAt(playerTransform);

            // �÷��̾���� �Ÿ��� ���
            float distanceToPlayer = Vector3.Distance(transform.position, Player.instance.transform.position);

            // �÷��̾ ������ ���� ���� ���� ���� �ְ�, ���� ��ٿ��� ������ �� ���� ����
            if (distanceToPlayer <= attackRange && Time.time - lastAttackTime >= attackCooldown)
            {
                isAttacking = true;
                lastAttackTime = Time.time;

                // �ִϸ��̼� ����
                animator.SetTrigger("Attack");
            }
        }
    }

    // Animation Event: Attack Event
    public void PerformAttack()
    {
        // �÷��̾�� �������� ���� (�÷��̾ ������ �ڽ� ��ü��� ����)
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