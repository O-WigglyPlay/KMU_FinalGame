using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float moveSpeed = 3f; // ������ �̵� �ӵ�
    public float attackRange = 2f; // ������ ���� ���� ����
    public float attackCooldown = 1f; // ���� ��ٿ� �ð�
    public float attackDamage = 10f; // ���� ������

    private Animator animator; // ������ �ִϸ�����
  //  private bool isAttacking = false; // ���� ������ ����
  //  private float lastAttackTime = 0f; // ������ ���� �ð�
    private Transform playerTransform; // �÷��̾��� Transform ������Ʈ

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // �ִϸ����� ������Ʈ ��������

        // �÷��̾� ������Ʈ�� �����Ͽ� �÷��̾��� Transform�� ������
        playerTransform = Player.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // �÷��̾ ���ϴ� ���� ���� ���
        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;

        // �÷��̾� �������� ���� �̵�
        transform.Translate(directionToPlayer * moveSpeed * Time.deltaTime);

        // �÷��̾���� �Ÿ��� ���
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
    }
}
