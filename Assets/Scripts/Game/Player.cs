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

    private Rigidbody2D rb_Player;
    private Animator p_Ani;
    private GameObject currentTree; // 현재 충돌 중인 나무 저장

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
            
            if (lastMoveX == -1)
            {
                attackTransform.localPosition = new Vector3(-0.033f, attackTransform.localPosition.y, attackTransform.localPosition.z);
                p_Ani.Play("Atk_left");
            }
            else if (lastMoveX == 1)
            {
                attackTransform.localPosition = new Vector3(0.6f, attackTransform.localPosition.y, attackTransform.localPosition.z);
                p_Ani.Play("Atk_right");
            }

            if (lastMoveY == 1)
            {
                EnableHitbox(top_Hitbox);
                DisableHitbox(down_Hitbox);
            }
            else if (lastMoveY == -1)
            {
                EnableHitbox(down_Hitbox);
                DisableHitbox(top_Hitbox);
            }
        }
    }

    private void EnableHitbox(GameObject hitbox)
    {
        var collider = hitbox.GetComponent<BoxCollider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;
            hitbox.SetActive(true);
            AttackCollision();
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

    private void PlayerMovement()
    {
        // 플레이어 이동 입력 처리
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // 입력을 기반으로 플레이어 이동 방향 설정
        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        // 입력에 따라 플레이어의 속도 설정
        rb_Player.velocity = movement * f_Speed;

        p_Ani.SetFloat("moveX", horizontalInput);
        p_Ani.SetFloat("moveY", verticalInput);
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 ||
            Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            p_Ani.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            p_Ani.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
        }
    }
    
    public void Die()
    {
        Debug.Log("Player Died");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            currentTree = collision.gameObject;  // 나무 오브젝트를 현재 충돌한 나무로 설정
            Debug.Log("나무와 충돌 시작: " + currentTree.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            if (currentTree == collision.gameObject)
            {
                Debug.Log("나무와 충돌 끝: " + currentTree.name);
                currentTree = null;  // 나무와의 충돌이 끝났다면 참조 제거
            }
        }
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
    }
}
