using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public int n_Hp;               // 플레이어 체력
    [SerializeField] public int n_maxHealth = 100;  // 플레이어 최대 체력
    [SerializeField] public float f_Speed;          // 플레이어 스피드
    [SerializeField] public int n_Dmg = 1;
    [SerializeField] private Transform attackTransform; // Attack 오브젝트의 Transform

    private Rigidbody2D rb_Player;
    private Animator p_Ani;
    private SpriteRenderer PlayerRenderer;
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
        PlayerRenderer = GetComponent<SpriteRenderer>();
        n_Hp = n_maxHealth;  // 시작할 때 체력 초기화
        n_Dmg = 1;

        if (attackTransform == null)
        {
            Debug.LogError("Attack Transform이 설정되지 않았습니다.");
            return;
        }

        // Attack 오브젝트의 Collider에 이벤트 등록
        var attackCollider = attackTransform.GetComponent<BoxCollider2D>();
        if (attackCollider != null)
        {
            attackCollider.isTrigger = true;
        }
        else
        {
            Debug.LogError("Attack 오브젝트에 BoxCollider2D가 없습니다.");
        }
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void Update()
    {
        if (currentTree != null)
        {
            Debug.Log("현재 충돌 중인 나무: " + currentTree.name);
        }

        if (Input.GetMouseButtonDown(0) && currentTree != null)  // 마우스 왼쪽 버튼 클릭 감지
        {
            Tree treeScript = currentTree.GetComponent<Tree>();
            if (treeScript != null)
            {
                treeScript.Tree_Hp -= n_Dmg;  // 나무의 체력 감소
                Debug.Log(treeScript.Tree_Hp);
            }
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
}
