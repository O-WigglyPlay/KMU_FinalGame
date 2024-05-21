using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animals : MonoBehaviour
{
    public int health = 100;
    public float moveSpeed; // ������ �̵� �ӵ�

    public GameObject meat; //������ �׿��� �� ������ ���
    public GameObject bone; //������ �׿��� �� ������ ��
    public GameObject leather; //������ �׿��� �� ������ ����
    public GameObject teeth; //������ �׿��� �� ������ �̻�


    public float meatDropChance = 0.25f; // ��� ������ ��� Ȯ��
    public float boneDropChance = 0.25f; // �� ������ ��� Ȯ��
    public float LeatherDropChance = 0.25f; // ���� ������ ��� Ȯ��
    public float teethDropChance = 0.25f; // ���� ������ ��� Ȯ��

    private Animator animator; // ������ �ִϸ�����
    private Transform playerTransform; // �÷��̾��� Transform ������Ʈ

    Rigidbody2D rigid;
    SpriteRenderer spriter;

    bool isLive = true;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();  //�浹 
        rigid.freezeRotation = true; // ȸ���� ����
        spriter = GetComponent<SpriteRenderer>();   //Sprite �ҷ�����
        animator = GetComponent<Animator>(); // �ִϸ����� ������Ʈ ��������
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // �浹�� ����� �÷��̾����� Ȯ��
        if (collision.gameObject.CompareTag("Player"))
        {
            // ������ ü���� ���ҽ�Ŵ
            health -= 10; // ���� ��� 10�� �������� �� �� ����

            if (health <= 0)
            {
                Die(); // ������ ����� ��� ó���ϴ� �Լ� ȣ��
            }
        }
    }

    // ������ ����� �� ȣ��Ǵ� �Լ�
    void Die()
    {
        // ���⿡ ������ �׾��� ���� ó���� �߰��� �� ����
        if(isLive)
        {
            isLive  = false;

            if(Random.value <= meatDropChance)
            {
                //��� ������ ����
                Instantiate(meat, transform.position, Quaternion.identity);
            }
            if (Random.value <= meatDropChance)
            {
                //�� ������ ����
                Instantiate(bone, transform.position, Quaternion.identity);
            }
            if (Random.value <= meatDropChance)
            {
                //���� ������ ����
                Instantiate(leather, transform.position, Quaternion.identity);
            }
            if (Random.value <= meatDropChance)
            {
                //�̻� ������ ����
                Instantiate(teeth, transform.position, Quaternion.identity);
            }
            Destroy(gameObject); // ���� ������Ʈ ����
        }
        
    }
}