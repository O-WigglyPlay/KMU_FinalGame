using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Animals : MonoBehaviour
{
    public int health = 100;
    public float moveSpeed; // ������ �̵� �ӵ�
    public float speedBoostMultiplier = 2.0f; // �ӵ� ���� ����
    public float speedBoostDuration = 2.0f; // �ӵ� ���� ���� �ð�
    public float directionChangeInterval;// ������ �����ϴ� ����

    public GameObject meat; //������ �׿��� �� ������ ���
    public GameObject bone; //������ �׿��� �� ������ ��
    public GameObject leather; //������ �׿��� �� ������ ����
    public GameObject teeth; //������ �׿��� �� ������ �̻�

    public float meatDropChance = 0.25f; // ��� ������ ��� Ȯ��
    public float boneDropChance = 0.25f; // �� ������ ��� Ȯ��
    public float leatherDropChance = 0.25f; // ���� ������ ��� Ȯ��
    public float teethDropChance = 0.25f; // ���� ������ ��� Ȯ��

    private Animator animator; // ������ �ִϸ�����
    private Transform playerTransform; // �÷��̾��� Transform ������Ʈ

    Rigidbody2D rigid;
    SpriteRenderer spriter;

    bool isLive = true;
    private Vector2 moveDirection;
    private float currentMoveSpeed;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();  //�浹 
        rigid.freezeRotation = true; // ȸ���� ����
        spriter = GetComponent<SpriteRenderer>();   //Sprite �ҷ�����
        animator = GetComponent<Animator>(); // �ִϸ����� ������Ʈ ��������
        currentMoveSpeed = moveSpeed; // �ʱ� �̵� �ӵ��� �⺻ �̵� �ӵ��� ����
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

        // ������ �������� �̵� ����
        moveDirection = Random.insideUnitCircle.normalized;
        rigid.velocity = moveDirection * moveSpeed;
        UpdateSpriteDirection();

        StartCoroutine(ChangeDirectionRoutine()); // ������ �ֱ������� �����ϴ� �ڷ�ƾ ����
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� ����� �÷��̾ �ƴ� ��� �ݴ� �������� �̵�
        if (!collision.gameObject.CompareTag("Player"))
        {
            Vector2 oppositeDirection = -moveDirection; // �ݴ� �������� �̵�
            rigid.velocity = oppositeDirection * moveSpeed;
            moveDirection = oppositeDirection; // �̵� ���� ������Ʈ
            UpdateSpriteDirection(); // ��������Ʈ ���� ������Ʈ
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
                StartCoroutine(TemporarySpeedBoost()); // �Ͻ����� �ӵ� ���� �ڷ�ƾ ����
                rigid.velocity = oppositeDirection * moveSpeed;
                moveDirection = oppositeDirection; // �̵� ���� ������Ʈ
                UpdateSpriteDirection(); // ��������Ʈ ���� ������Ʈ
            }
        }
    }

    // �Ͻ������� �ӵ��� ������Ű�� �ڷ�ƾ
    IEnumerator TemporarySpeedBoost()
    {
        currentMoveSpeed = moveSpeed * speedBoostMultiplier; // �ӵ� ����
        yield return new WaitForSeconds(speedBoostDuration); // ���� �ð� ���
        currentMoveSpeed = moveSpeed; // ���� �ӵ��� ����
    }

    // ������ �ֱ������� �����ϴ� �ڷ�ƾ
    IEnumerator ChangeDirectionRoutine()
    {
        while (isLive)
        {
            yield return new WaitForSeconds(directionChangeInterval); // ���� �ð� ���
            ChangeDirection(); // ���� ����
        }
    }

    // ������ �����ϴ� �Լ�
    void ChangeDirection()
    {
        moveDirection = Random.insideUnitCircle.normalized; // ���ο� ���� ���� ����
        rigid.velocity = moveDirection * currentMoveSpeed; // ���ο� �������� �̵�
        UpdateSpriteDirection(); // ��������Ʈ ���� ������Ʈ
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
                    // ��� ������ ����
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
                    // ��� ������ ����
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
                    // ��� ������ ����
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