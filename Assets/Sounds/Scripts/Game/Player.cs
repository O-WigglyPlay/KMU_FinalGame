using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public int n_Hp;               // 플레이어 체력
    public int n_maxHealth = 100;  // 플레이어 최대 체력
    public float f_Speed;          // 플레이어 스피드
    private bool isRunning = false;// 달리는 중인지 확인용
    private Rigidbody2D rb_Player;
    private Animator p_Ani;
    private SpriteRenderer PlayerRenderer;

    public static Player instance;

    public void Awake()
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
        n_Hp = n_maxHealth;  // 시작할 때 체력 동급
        PlayerRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        // 플레이어가 존재하고 플레이어 컨트롤러가 초기화되었는지 확인
        if (Player.instance != null)
        {
            FlipPlayer();
        }
    }

    private void FixedUpdate()
    {
        PlayerMovement();
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

    void FlipPlayer()
    {
        bool isMoving = p_Ani.GetFloat("moveX") != 0 || p_Ani.GetFloat("moveY") != 0;

        if (isMoving)
        {
            // 플레이어의 위치와 마우스의 위치를 비교하여 플레이어가 어느 방향을 바라보는지 판단
            Vector3 playerPosition = transform.position;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 마우스 위치에 따라 플레이어의 스프라이트를 좌우 반전
            if (mousePosition.x < playerPosition.x)
            {
                transform.localScale = new Vector3(-1, 1, 1); // 좌우 반전
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1); // 원래 방향
            }
        }
    }

    public void Die()
    {
        Debug.Log("Player Died");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 오브젝트가 Ghost 또는 Zombie인지 확인
        if (collision.gameObject.CompareTag("Ghost") || collision.gameObject.CompareTag("Zombie"))
        {
            // 충돌 시 피 감소
            n_Hp -= 10; // 예를 들어 10의 데미지를 입힘

            // 피가 0 이하로 떨어졌을 때 게임 오버
            if (n_Hp <= 0)
            {
                Die();
            }
        }
    }
}