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
    private Rigidbody2D rb; // Rigidbody2D ������Ʈ
    private SpriteRenderer spriteRenderer; // SpriteRenderer ������Ʈ

    private void Start()
    {
        animator = GetComponent<Animator>(); // �ִϸ����� ������Ʈ ��������
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D ������Ʈ ��������
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer ������Ʈ ��������

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
        rb.velocity = directionToPlayer * moveSpeed;

        // ������ �÷��̾ �ٶ󺸵��� ��������Ʈ ������ (�ݴ� ����)
        if (directionToPlayer.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        // �÷��̾���� �Ÿ��� ���
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // �÷��̾ ������ ���� ���� ���� ���� �ְ�, ���� ��ٿ��� ������ �� ���� ����
        if (distanceToPlayer <= attackRange && Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;

            // �ִϸ��̼� ����
            animator.SetTrigger("Attack");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� ����� �÷��̾����� Ȯ��
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾��� Rigidbody2D�� Kinematic���� �����Ͽ� ������ �浹 ������ ����
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.bodyType = RigidbodyType2D.Kinematic;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // �浹�� ����� �÷��̾����� Ȯ��
        if (collision.gameObject.CompareTag("Player"))
        {
            // ���� ��ٿ� �ð��� �����ٸ� ü�� ����
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                lastAttackTime = Time.time;

                // �÷��̾��� ü���� ���ҽ�Ű�� ����
                Player player = collision.gameObject.GetComponent<Player>();
                if (player != null)
                {
                    player.n_Hp -= attackDamage;

                    // �÷��̾� ü���� 0 �������� Ȯ���Ͽ� ��� ó��
                    if (player.n_Hp <= 0)
                    {
                        player.Die();
                    }
                }

                // �ִϸ��̼� ����
                animator.SetTrigger("Attack");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // �浹�� ����� �÷��̾����� Ȯ��
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾��� Rigidbody2D�� �ٽ� Dynamic���� �����Ͽ� ���� ���·� ����
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾��� ���ݰ� �浹�ߴ��� Ȯ��
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            Debug.Log("�÷��̾��� ���ݰ� �浹");
            // ������ ü���� ���ҽ�Ű�� ����
            TakeDamage(Player.instance.n_Dmg);
        }
    }

    private void TakeDamage(int damage)
    {
        curHealth -= damage;
        Debug.Log("���� ü�� ����: " + curHealth);

        // ���� ü���� 0 �������� Ȯ���Ͽ� ��� ó��
        if (curHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // ���� ��� ó�� ����
        animator.SetTrigger("Die");
        rb.velocity = Vector2.zero;
        // ���� �ð� �� ���� ������Ʈ ��Ȱ��ȭ
        Invoke("DeactivateGhost", 1f);
    }

    private void DeactivateGhost()
    {
        gameObject.SetActive(false);
    }
}