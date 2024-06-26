using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Animals : MonoBehaviour
{
    public int health = 100;
    public float moveSpeed; // 동물의 이동 속도
    public float speedBoostMultiplier = 2.0f; // 속도 증가 배율
    public float speedBoostDuration = 2.0f; // 속도 증가 지속 시간
    public float directionChangeInterval;// 방향을 변경하는 간격

    public GameObject meat; //동물을 죽였을 때 나오는 고기
    public GameObject bone; //동물을 죽였을 때 나오는 뼈
    public GameObject leather; //동물을 죽였을 때 나오는 가죽
    public GameObject teeth; //동물을 죽였을 때 나오는 이빨

    public float meatDropChance = 0.25f; // 고기 아이템 드랍 확률
    public float boneDropChance = 0.25f; // 뼈 아이템 드랍 확률
    public float leatherDropChance = 0.25f; // 가죽 아이템 드랍 확률
    public float teethDropChance = 0.25f; // 가죽 아이템 드랍 확률

    private Animator animator; // 동물의 애니메이터
    private Transform playerTransform; // 플레이어의 Transform 컴포넌트

    Rigidbody2D rigid;
    SpriteRenderer spriter;

    bool isLive = true;
    private Vector2 moveDirection;
    private float currentMoveSpeed;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();  //충돌 
        rigid.freezeRotation = true; // 회전을 고정
        spriter = GetComponent<SpriteRenderer>();   //Sprite 불러오기
        animator = GetComponent<Animator>(); // 애니메이터 컴포넌트 가져오기
        currentMoveSpeed = moveSpeed; // 초기 이동 속도를 기본 이동 속도로 설정
    }

    void Update()
    {
        // 이동 방향 업데이트 및 스프라이트 방향 설정
        if (rigid.velocity != Vector2.zero)
        {
            moveDirection = rigid.velocity.normalized;
            UpdateSpriteDirection();
        }
    }

    void Start()
    {
        // 플레이어 찾기
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }

        // 랜덤한 방향으로 이동 시작
        moveDirection = Random.insideUnitCircle.normalized;
        rigid.velocity = moveDirection * moveSpeed;
        UpdateSpriteDirection();

        StartCoroutine(ChangeDirectionRoutine()); // 방향을 주기적으로 변경하는 코루틴 시작
    }

    private void UpdateSpriteDirection()
    {
        if (moveDirection.x > 0)
        {
            spriter.flipX = false; // 오른쪽으로 이동 중
        }
        else if (moveDirection.x < 0)
        {
            spriter.flipX = true; // 왼쪽으로 이동 중
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 대상이 플레이어가 아닌 경우 반대 방향으로 이동
        if (!collision.gameObject.CompareTag("Player"))
        {
            Vector2 oppositeDirection = -moveDirection; // 반대 방향으로 이동
            rigid.velocity = oppositeDirection * moveSpeed;
            moveDirection = oppositeDirection; // 이동 방향 업데이트
            UpdateSpriteDirection(); // 스프라이트 방향 업데이트
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // 충돌한 대상이 플레이어인지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            // 동물의 체력을 감소시킴
            health -= 10; // 예를 들어 10의 데미지를 줄 수 있음

            if (health <= 0)
            {
                Die(); // 동물이 사망할 경우 처리하는 함수 호출
            }
            else
            {
                // 반대 방향으로 도망
                Vector2 oppositeDirection = (transform.position - playerTransform.position).normalized;
                StartCoroutine(TemporarySpeedBoost()); // 일시적인 속도 증가 코루틴 시작
                rigid.velocity = oppositeDirection * moveSpeed;
                moveDirection = oppositeDirection; // 이동 방향 업데이트
                UpdateSpriteDirection(); // 스프라이트 방향 업데이트
            }
        }
    }

    // 일시적으로 속도를 증가시키는 코루틴
    IEnumerator TemporarySpeedBoost()
    {
        currentMoveSpeed = moveSpeed * speedBoostMultiplier; // 속도 증가
        yield return new WaitForSeconds(speedBoostDuration); // 일정 시간 대기
        currentMoveSpeed = moveSpeed; // 원래 속도로 복원
    }

    // 방향을 주기적으로 변경하는 코루틴
    IEnumerator ChangeDirectionRoutine()
    {
        while (isLive)
        {
            yield return new WaitForSeconds(directionChangeInterval); // 일정 시간 대기
            ChangeDirection(); // 방향 변경
        }
    }

    // 방향을 변경하는 함수
    void ChangeDirection()
    {
        moveDirection = Random.insideUnitCircle.normalized; // 새로운 랜덤 방향 설정
        rigid.velocity = moveDirection * currentMoveSpeed; // 새로운 방향으로 이동
        UpdateSpriteDirection(); // 스프라이트 방향 업데이트
    }


    // 동물이 사망할 때 호출되는 함수
    void Die()
    {
        // 여기에 동물이 죽었을 때의 처리를 추가할 수 있음
        if(isLive)
        {
            isLive  = false;

            List<Vector3> itemPositions = new List<Vector3>(); // 이미 생성된 아이템들의 위치를 저장할 리스트

            Vector3 spawnPosition = transform.position; // 초기 생성 위치

            if (Random.value <= meatDropChance)
            {
                {
                    // 고기 아이템 생성
                    bool canSpawn = true;

                    // 이미 생성된 아이템들과의 거리를 확인하여 충돌을 피함
                    foreach (Vector3 position in itemPositions)
                    {
                        if (Vector3.Distance(spawnPosition, position) < 1.0f) // 일정 거리 이내에 아이템이 있으면
                        {
                            canSpawn = false; // 생성 불가능
                            break;
                        }
                    }

                    if (canSpawn)
                    {
                        // 아이템 생성 가능한 경우
                        Instantiate(meat, spawnPosition, Quaternion.identity);
                        itemPositions.Add(spawnPosition); // 생성된 아이템의 위치를 리스트에 추가
                    }
                }   
            }
            if (Random.value <= boneDropChance)
            {
                {
                    // 뼈 아이템 생성
                    bool canSpawn = true;

                    // 이미 생성된 아이템들과의 거리를 확인하여 충돌을 피함
                    foreach (Vector3 position in itemPositions)
                    {
                        if (Vector3.Distance(spawnPosition, position) < 1.0f) // 일정 거리 이내에 아이템이 있으면
                        {
                            canSpawn = false; // 생성 불가능
                            break;
                        }
                    }

                    if (canSpawn)
                    {
                        // 아이템 생성 가능한 경우
                        Instantiate(bone, spawnPosition, Quaternion.identity);
                        itemPositions.Add(spawnPosition); // 생성된 아이템의 위치를 리스트에 추가
                    }
                }
            }
            if (Random.value <= leatherDropChance)
            {
                {
                    // 고기 아이템 생성
                    bool canSpawn = true;

                    // 이미 생성된 아이템들과의 거리를 확인하여 충돌을 피함
                    foreach (Vector3 position in itemPositions)
                    {
                        if (Vector3.Distance(spawnPosition, position) < 1.0f) // 일정 거리 이내에 아이템이 있으면
                        {
                            canSpawn = false; // 생성 불가능
                            break;
                        }
                    }

                    if (canSpawn)
                    {
                        // 아이템 생성 가능한 경우
                        Instantiate(leather, spawnPosition, Quaternion.identity);
                        itemPositions.Add(spawnPosition); // 생성된 아이템의 위치를 리스트에 추가
                    }
                }
            }
            if (Random.value <= teethDropChance)
            {
                {
                    // 고기 아이템 생성
                    bool canSpawn = true;

                    // 이미 생성된 아이템들과의 거리를 확인하여 충돌을 피함
                    foreach (Vector3 position in itemPositions)
                    {
                        if (Vector3.Distance(spawnPosition, position) < 1.0f) // 일정 거리 이내에 아이템이 있으면
                        {
                            canSpawn = false; // 생성 불가능
                            break;
                        }
                    }

                    if (canSpawn)
                    {
                        // 아이템 생성 가능한 경우
                        Instantiate(teeth, spawnPosition, Quaternion.identity);
                        itemPositions.Add(spawnPosition); // 생성된 아이템의 위치를 리스트에 추가
                    }
                }
            }
            Destroy(gameObject); // 동물 오브젝트 삭제
        }
        
    }
}