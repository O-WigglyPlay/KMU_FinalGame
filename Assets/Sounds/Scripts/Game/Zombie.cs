using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float moveSpeed; // ������ �̵� �ӵ�
    public float attackRange = 2f; // ������ ���� ���� ����
    public float attackCooldown = 1f; // ���� ��ٿ� �ð�
    public float attackDamage = 10f; // ���� ������

    public Rigidbody2D target;

    public GameObject ZombieArm;   //�� ������
    public GameObject ZombieBody;   //���� ������
    public GameObject ZombieHead;  //�Ӹ� ������
    public GameObject ZombieFeet;  //�� ������

    public float armDropChance = 0.25f; // �� ������ ��� Ȯ��
    public float bodyDropChance = 0.25f; // ���� ������ ��� Ȯ��
    public float headDropChance = 0.25f; // �Ӹ� ������ ��� Ȯ��
    public float legDropChance = 0.25f; // �ٸ� ������ ��� Ȯ��

    bool isLive = true;
    bool isAttacking = false;
    float lastAttackTime;

    Rigidbody2D rigid;
    SpriteRenderer spriter;

    private Animator animator; // ������ �ִϸ�����
    private Transform playerTransform; // �÷��̾��� Transform ������Ʈ

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true; // ȸ���� ����
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // �ִϸ����� ������Ʈ ��������
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isLive || isAttacking)
        {
            return;
        }
        Vector2 dirvec = target.position - rigid.position;
        Vector2 nextVec = dirvec.normalized * moveSpeed * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;

        // ���Ͱ� �÷��̾ ���� �ִ��� Ȯ���ϰ� ������ ����
        spriter.flipX = target.position.x < rigid.position.x;

        // �÷��̾���� �Ÿ� ���
        float distanceToPlayer = Vector2.Distance(rigid.position, target.position);

        // �÷��̾���� �Ÿ��� ���� ���� ���� �̳��̰� ���� ��ٿ��� �������� ����
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            StartCoroutine(Attack());
        }
        else
        {
            // ���� ���� ���� �̳��� �ƴϸ� ���͸� �÷��̾� ������ �̵���Ŵ
            rigid.MovePosition(rigid.position + nextVec);
            rigid.velocity = Vector2.zero;
        }
    }

    public void Die()
    {
        if (isLive)
        {
            isLive = false; // ���� ���¸� false�� ����

            // ������ ��� Ȯ���� üũ�Ͽ� �������� �������� ����
            if (Random.value <= armDropChance)
            {
                // �� ������ ����
                Instantiate(ZombieArm, transform.position, Quaternion.identity);
            }
            if (Random.value <= bodyDropChance)
            {
                // ���� ������ ����
                Instantiate(ZombieBody, transform.position, Quaternion.identity);
            }
            if (Random.value <= headDropChance)
            {
                // �Ӹ� ������ ����
                Instantiate(ZombieHead, transform.position, Quaternion.identity);
            }
            if (Random.value <= legDropChance)
            {
                // �ٸ� ������ ����
                Instantiate(ZombieFeet, transform.position, Quaternion.identity);
            }

            // �� ������Ʈ�� �ı�
            Destroy(gameObject);
        }
    }
    // ���� �ڷ�ƾ
    IEnumerator Attack()
    {
        isAttacking = true; // ���� �� ���·� ����
        animator.SetBool("ZombieAttack", isAttacking); // ���� �ִϸ��̼� ���

        float attackAnimationLength = 1.0f; // ���� �ִϸ��̼��� ���� ���̷� ����
        // ���� ����� ���� ������ ���
        yield return new WaitForSeconds(attackAnimationLength);

        // ���� �� ��ٿ� ����
        lastAttackTime = Time.time;
        isAttacking = false; // ���� ���� ���·� ����
        animator.SetBool("ZombieAttack", isAttacking); // ���� �ִϸ��̼� ���
    }

}
