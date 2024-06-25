using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int n_Hp;               // 플레이어 체력
    public int n_maxHealth = 100;  // 플레이어 최대 체력
    public float f_Speed;          // 플레이어 스피드
    public int n_Dmg = 1;

    [SerializeField] private Transform attackTransform; // Attack 오브젝트의 Transform
    [SerializeField] private GameObject top_Hitbox; // Top 히트박스 오브젝트
    [SerializeField] private GameObject down_Hitbox; // Down 히트박스 오브젝트
    [SerializeField] private GameObject woodWeapon; // 나무 무기 오브젝트
    [SerializeField] private GameObject mineWeapon; // 광물 무기 오브젝트

    private Rigidbody2D rb_Player;
    private Animator p_Ani;
    private GameObject currentTree; // 현재 충돌 중인 나무 저장
    private GameObject currentMineral; // 현재 충돌 중인 광물 저장
    private bool isColliding = false; // 충돌 상태를 저장하는 변수
    private Vector2 lastMoveDirection; // 마지막 이동 방향

    public static Player instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        rb_Player = GetComponent<Rigidbody2D>();
        p_Ani = GetComponent<Animator>();
        n_Hp = n_maxHealth;  // 시작할 때 체력 초기화
        n_Dmg = 1;

        var attackCollider = attackTransform.GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float lastMoveX = p_Ani.GetFloat("lastMoveX");
            float lastMoveY = p_Ani.GetFloat("lastMoveY");

            DisableAllHitboxes(); // 모든 히트박스를 비활성화합니다.

            // 좌우 공격
            if (lastMoveX == -1)
            {
                attackTransform.localPosition = new Vector3(-0.033f, attackTransform.localPosition.y, attackTransform.localPosition.z);
                p_Ani.Play("Atk_left");
                EnableHitbox(attackTransform.gameObject); // 왼쪽 공격 시 attackTransform 히트박스를 활성화합니다.
            }
            else if (lastMoveX == 1)
            {
                attackTransform.localPosition = new Vector3(0.6f, attackTransform.localPosition.y, attackTransform.localPosition.z);
                p_Ani.Play("Atk_right");
                EnableHitbox(attackTransform.gameObject); // 오른쪽 공격 시 attackTransform 히트박스를 활성화합니다.
            }

            // 상하 공격
            if (lastMoveY == 1)
            {
                EnableHitbox(top_Hitbox);
                p_Ani.Play("Atk_top");  // 위쪽 공격 애니메이션 실행
            }
            else if (lastMoveY == -1)
            {
                EnableHitbox(down_Hitbox);
                p_Ani.Play("Atk_bottom");  // 아래쪽 공격 애니메이션 실행
            }

            AttackCollision(); // 공격 충돌 검사를 수행합니다.
        }
    }

    private void EnableHitbox(GameObject hitbox)
    {
        var collider = hitbox.GetComponent<BoxCollider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;
            hitbox.SetActive(true);
        }
    }

    private void DisableHitbox(GameObject hitbox)
    {
        var collider = hitbox.GetComponent<BoxCollider2D>();
        if (collider != null)
        {
            collider.isTrigger = false;
            hitbox.SetActive(false);
        }
    }

    private void DisableAllHitboxes()
    {
        DisableHitbox(top_Hitbox);
        DisableHitbox(down_Hitbox);
        DisableHitbox(attackTransform.gameObject); // attackTransform 히트박스도 비활성화합니다.
    }

    private void PlayerMovement()
    {
        // 플레이어 이동 입력 처리
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // 입력을 기반으로 플레이어 이동 방향 설정
        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        // 입력에 따라 플레이어의 속도 설정
        rb_Player.velocity = movement * f_Speed;

        if (movement != Vector2.zero)
        {
            lastMoveDirection = movement;
        }

        p_Ani.SetFloat("moveX", horizontalInput);
        p_Ani.SetFloat("moveY", verticalInput);
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 ||
            Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            p_Ani.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            p_Ani.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
        }
    }

    public Vector2 GetLastMoveDirection()
    {
        return lastMoveDirection.normalized; // 방향을 정규화하여 반환
    }

    public void Die()
    {
        Debug.Log("Player Died");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isColliding = true;
        if (collision.gameObject.CompareTag("Tree"))
        {
            currentTree = collision.gameObject;  // 나무 오브젝트를 현재 충돌한 나무로 설정
            woodWeapon.SetActive(true);
            mineWeapon.SetActive(false);
            Debug.Log("나무와 충돌 시작: " + currentTree.name);
        }
        else if (collision.gameObject.CompareTag("Mineral"))
        {
            currentMineral = collision.gameObject;  // 광물 오브젝트를 현재 충돌한 광물로 설정
            woodWeapon.SetActive(false);
            mineWeapon.SetActive(true);
            Debug.Log("광물과 충돌 시작: " + currentMineral.name);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tree") || collision.gameObject.CompareTag("Mineral"))
        {
            isColliding = true;  // 충돌 상태를 지속적으로 갱신
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isColliding = false;
        if (collision.gameObject.CompareTag("Tree"))
        {
            if (currentTree == collision.gameObject)
            {
                Debug.Log("나무와 충돌 끝: " + currentTree.name);
                currentTree = null;  // 나무와의 충돌이 끝났다면 참조 제거
                woodWeapon.SetActive(false); // 나무 무기 비활성화
            }
        }
        else if (collision.gameObject.CompareTag("Mineral"))
        {
            if (currentMineral == collision.gameObject)
            {
                Debug.Log("광물과 충돌 끝: " + currentMineral.name);
                currentMineral = null;  // 광물과의 충돌이 끝났다면 참조 제거
                mineWeapon.SetActive(false); // 광물 무기 비활성화
            }
        }
    }

    public bool IsColliding()
    {
        return isColliding; // 충돌 상태가 유지되는지 확인
    }

    public void AttackCollision()
    {
        Debug.Log("충돌 발생");

        if (currentTree != null)
        {
            TreeMng treeScript = currentTree.GetComponent<TreeMng>();
            if (treeScript != null)
            {
                treeScript.Tree_Hp -= n_Dmg;  // 나무의 체력 감소
                Debug.Log("충돌로 인한 나무 체력 감소: " + treeScript.Tree_Hp);
            }
        }
        else if (currentMineral != null)
        {
            MChange mineralScript = currentMineral.GetComponent<MChange>();
            if (mineralScript != null)
            {
                mineralScript.TakeDamage(n_Dmg);  // 광물의 체력 감소
                if (mineralScript.Mineral_Hp <= 0)
                {
                    currentMineral = null; // 파괴 후 currentMineral 참조 제거
                }
            }
        }
    }
}
