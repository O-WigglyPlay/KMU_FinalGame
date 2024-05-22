using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Animals : MonoBehaviour
{
    public int health = 100;
    public float moveSpeed; // ������ �̵� �ӵ�

    public GameObject meat; //������ �׿��� �� ������ ����
    public GameObject bone; //������ �׿��� �� ������ ��
    public GameObject leather; //������ �׿��� �� ������ ����
    public GameObject teeth; //������ �׿��� �� ������ �̻�


    public float meatDropChance = 0.25f; // ���� ������ ��� Ȯ��
    public float boneDropChance = 0.25f; // �� ������ ��� Ȯ��
    public float leatherDropChance = 0.25f; // ���� ������ ��� Ȯ��
    public float teethDropChance = 0.25f; // ���� ������ ��� Ȯ��

    private Animator animator; // ������ �ִϸ�����
    private Transform playerTransform; // �÷��̾��� Transform ������Ʈ

    Rigidbody2D rigid;
    SpriteRenderer spriter;

    bool isLive = true;
    private Vector2 moveDirection;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();  //�浹 
        rigid.freezeRotation = true; // ȸ���� ����
        spriter = GetComponent<SpriteRenderer>();   //Sprite �ҷ�����
        animator = GetComponent<Animator>(); // �ִϸ����� ������Ʈ ��������
    }

    void Update()
    {
        // �̵� ���� ������Ʈ �� ��������Ʈ ���� ����
        if (rigid.velocity != Vector2.zero)
        {
            moveDirection = rigid.velocity.normalized;
            UpdateSpriteDirection();
        }
    }

    void Start()
    {
        // �÷��̾� ã��
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found in the scene.");
        }

        // ������ �������� �̵� ����
        moveDirection = Random.insideUnitCircle.normalized;
        rigid.velocity = moveDirection * moveSpeed;
        UpdateSpriteDirection();
    }

    private void UpdateSpriteDirection()
    {
        if (moveDirection.x > 0)
        {
            spriter.flipX = false; // ���������� �̵� ��
        }
        else if (moveDirection.x < 0)
        {
            spriter.flipX = true; // �������� �̵� ��
        }
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
            else
            {
                // �ݴ� �������� ����
                Vector2 oppositeDirection = (transform.position - playerTransform.position).normalized;
                rigid.velocity = oppositeDirection * moveSpeed;
                moveDirection = oppositeDirection; // �̵� ���� ������Ʈ
                UpdateSpriteDirection(); // ��������Ʈ ���� ������Ʈ
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

            List<Vector3> itemPositions = new List<Vector3>(); // �̹� ������ �����۵��� ��ġ�� ������ ����Ʈ

            Vector3 spawnPosition = transform.position; // �ʱ� ���� ��ġ

            if (Random.value <= meatDropChance)
            {
                {
                    // ���� ������ ����
                    bool canSpawn = true;

                    // �̹� ������ �����۵���� �Ÿ��� Ȯ���Ͽ� �浹�� ����
                    foreach (Vector3 position in itemPositions)
                    {
                        if (Vector3.Distance(spawnPosition, position) < 1.0f) // ���� �Ÿ� �̳��� �������� ������
                        {
                            canSpawn = false; // ���� �Ұ���
                            break;
                        }
                    }

                    if (canSpawn)
                    {
                        // ������ ���� ������ ���
                        Instantiate(meat, spawnPosition, Quaternion.identity);
                        itemPositions.Add(spawnPosition); // ������ �������� ��ġ�� ����Ʈ�� �߰�
                    }
                }   
            }
            if (Random.value <= boneDropChance)
            {
                {
                    // �� ������ ����
                    bool canSpawn = true;

                    // �̹� ������ �����۵���� �Ÿ��� Ȯ���Ͽ� �浹�� ����
                    foreach (Vector3 position in itemPositions)
                    {
                        if (Vector3.Distance(spawnPosition, position) < 1.0f) // ���� �Ÿ� �̳��� �������� ������
                        {
                            canSpawn = false; // ���� �Ұ���
                            break;
                        }
                    }

                    if (canSpawn)
                    {
                        // ������ ���� ������ ���
                        Instantiate(bone, spawnPosition, Quaternion.identity);
                        itemPositions.Add(spawnPosition); // ������ �������� ��ġ�� ����Ʈ�� �߰�
                    }
                }
            }
            if (Random.value <= leatherDropChance)
            {
                {
                    // ���� ������ ����
                    bool canSpawn = true;

                    // �̹� ������ �����۵���� �Ÿ��� Ȯ���Ͽ� �浹�� ����
                    foreach (Vector3 position in itemPositions)
                    {
                        if (Vector3.Distance(spawnPosition, position) < 1.0f) // ���� �Ÿ� �̳��� �������� ������
                        {
                            canSpawn = false; // ���� �Ұ���
                            break;
                        }
                    }

                    if (canSpawn)
                    {
                        // ������ ���� ������ ���
                        Instantiate(leather, spawnPosition, Quaternion.identity);
                        itemPositions.Add(spawnPosition); // ������ �������� ��ġ�� ����Ʈ�� �߰�
                    }
                }
            }
            if (Random.value <= teethDropChance)
            {
                {
                    // ���� ������ ����
                    bool canSpawn = true;

                    // �̹� ������ �����۵���� �Ÿ��� Ȯ���Ͽ� �浹�� ����
                    foreach (Vector3 position in itemPositions)
                    {
                        if (Vector3.Distance(spawnPosition, position) < 1.0f) // ���� �Ÿ� �̳��� �������� ������
                        {
                            canSpawn = false; // ���� �Ұ���
                            break;
                        }
                    }

                    if (canSpawn)
                    {
                        // ������ ���� ������ ���
                        Instantiate(teeth, spawnPosition, Quaternion.identity);
                        itemPositions.Add(spawnPosition); // ������ �������� ��ġ�� ����Ʈ�� �߰�
                    }
                }
            }
            Destroy(gameObject); // ���� ������Ʈ ����
        }
        
    }
}