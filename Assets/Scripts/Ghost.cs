using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float moveSpeed; // ������ �̵� �ӵ�
    public float attackRange = 2f; // ������ ���� ���� ����
    public float attackCooldown = 1f; // ���� ��ٿ� �ð�
    public float attackDamage = 10f; // ���� ������

    private Animator animator; // ������ �ִϸ�����
    //private bool isAttacking = false; // ���� ������ ����
    private float lastAttackTime = 0f; // ������ ���� �ð�
    private Transform playerTransform; // �÷��̾��� Transform ������Ʈ

    private void Start()
    {
        animator = GetComponent<Animator>(); // �ִϸ����� ������Ʈ ��������

        // �÷��̾� ������Ʈ�� �����Ͽ� �÷��̾��� Transform�� ������
        playerTransform = Player.instance.transform;
    }

    private void Update()
    {
        // �÷��̾ ���ϴ� ���� ���� ���
        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;

        // �÷��̾� �������� ���� �̵�
        transform.Translate(directionToPlayer * moveSpeed * Time.deltaTime);

        // �÷��̾���� �Ÿ��� ���
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // �÷��̾ ������ ���� ���� ���� ���� �ְ�, ���� ��ٿ��� ������ �� ���� ����
        if (distanceToPlayer <= attackRange && Time.time - lastAttackTime >= attackCooldown)
        {
            //isAttacking = true;
            lastAttackTime = Time.time;

            // �ִϸ��̼� ����
            animator.SetTrigger("Attack");
        }
    }

    // Animation Event: Attack Event
    public void PerformAttack()
    {
        // �÷��̾�� �������� ���� (�÷��̾ ������ �ڽ� ��ü��� ����)
        Player playerScript = Player.instance;
        if (playerScript != null)
        {
            playerScript.TakeDamage(attackDamage);
        }
    }

    // Animation Event: End Attack
    public void EndAttack()
    {
        //isAttacking = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ���� �浹�� ��ü�� �÷��̾���, �������� �ֵ��� ��
        if (other.CompareTag("Player"))
        {
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
            {
                // �÷��̾�� �������� ��
                playerScript.TakeDamage(attackDamage);

                // ���� ������ ���� ��ٿ� ����
                StartCoroutine(AttackCooldown());
            }
        }
    }

    private IEnumerator AttackCooldown()
    {
        // ������ ��ٿ� �ð����� ���
        yield return new WaitForSeconds(attackCooldown);

        // ������ ���� �ð� �ʱ�ȭ
        lastAttackTime = Time.time;
    }
}