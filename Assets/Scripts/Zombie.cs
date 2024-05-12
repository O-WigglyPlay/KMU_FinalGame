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

    bool isLive = true;

    Rigidbody2D rigid;
    SpriteRenderer spriter;

    private Animator animator; // ������ �ִϸ�����
    private Transform playerTransform; // �÷��̾��� Transform ������Ʈ

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // �ִϸ����� ������Ʈ ��������
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isLive)
        {
            return;
        }
        Vector2 dirvec = target.position - rigid.position;
        Vector2 nextVec = dirvec.normalized * moveSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!isLive)
        {
            return;
        }

        spriter.flipX = target.position.x < rigid.position.y;
    }
}
