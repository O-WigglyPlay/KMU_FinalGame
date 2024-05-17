using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public int maxHealth;  //�ִ� ü��
    public int curHealth;  //���� ü��

    public float moveSpeed; // ������ �̵� �ӵ�
    public float attackRange; // ������ ���� ���� ����
    public float attackCooldown; // ���� ��ٿ� �ð�
    public int attackDamage; // ���� ������

    private Animator animator; // ������ �ִϸ�����
    private float lastAttackTime = 0f; // ������ ���� �ð�
    private Transform playerTransform; // �÷��̾��� Transform ������Ʈ

    private void Start()
    {
        animator = GetComponent<Animator>(); // �ִϸ����� ������Ʈ ��������

        // �÷��̾� ������Ʈ�� �����Ͽ� �÷��̾��� Transform�� ������
        playerTransform = Player.instance.transform;

        // �ʱ� ü�� ����
        curHealth = maxHealth;
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
}