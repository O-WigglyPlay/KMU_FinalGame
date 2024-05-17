using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class LongAttackZB : MonoBehaviour
{
    public float moveSpeed; // ������ �̵� �ӵ�
    public float attackRange = 5f; // ���Ÿ� ���� ����
    public float attackCooldown = 3f; // ���� ��ٿ� �ð�
    public GameObject ZBSpitPrefab; // ����ü ������

    public Rigidbody2D target;

    Rigidbody2D rigid;
    SpriteRenderer spriter;

    bool isLive = true;
    bool isAttacking = false;
    float lastAttackTime;

    private Animator animator; // ������ �ִϸ�����
    private Transform playerTransform; // �÷��̾��� Transform ������Ʈ

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true; // ȸ���� ����
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // �ִϸ����� ������Ʈ ��������
    }

    void FixedUpdate()
    {
        if (!isLive || isAttacking)
        {
            return;
        }
        Vector2 dirvec = target.position - rigid.position;
        Vector2 nextVec = dirvec.normalized * moveSpeed * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;

        // ���Ͱ� �÷��̾ ���� �ִ��� Ȯ���ϰ� ������ ����
        spriter.flipX = target.position.x > rigid.position.x;

        // �÷��̾���� �Ÿ� ���
        float distanceToPlayer = Vector2.Distance(rigid.position, target.position);

        // �÷��̾���� �Ÿ��� ���Ÿ� ���� ���� �̳��̰� ���� ��ٿ��� �������� ����
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            StartCoroutine(RangedAttack());
        }
    }

    IEnumerator RangedAttack()
    {
        isAttacking = true;
        animator.SetBool("ZombieLongA", isAttacking); // ���Ÿ� ���� �ִϸ��̼� ���

        float attackAnimationLength = 1.0f; // ���Ÿ� ���� �ִϸ��̼� ����
        yield return new WaitForSeconds(attackAnimationLength / 2); // �ִϸ��̼� �߰��� ����ü �߻�

        Vector2 direction = (target.position - rigid.position).normalized;
        GameObject ZBSpit = Instantiate(ZBSpitPrefab, transform.position, Quaternion.identity);
        ZBSpit.GetComponent<SpitCtrl>().SetDirection(direction); // ����ü�� ���� ����

        lastAttackTime = Time.time;
        yield return new WaitForSeconds(attackAnimationLength / 2);

        isAttacking = false;
        animator.SetBool("ZombieLongA", isAttacking);
    }

}