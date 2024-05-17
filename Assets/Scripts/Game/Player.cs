using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int n_Hp;               // 플레이어 체력
    public int n_maxHealth = 100;   // 플레이어 최대 체력
    public float f_Speed;           // 플레이어 스피드
    public int n_AttackDmg = 1;
    private bool isRunning = false;
    private Rigidbody2D rb_Player;
    private Animator p_Ani;
    private SpriteRenderer PlayerRenderer;

    public void Awake()
    {
        rb_Player = GetComponent<Rigidbody2D>();
        p_Ani = GetComponent<Animator>();
        PlayerRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        n_Hp = n_maxHealth;  // 시작할 때 체력 동급
    }

    private void Update()
    {
        Attack();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    // Player 이동 처리
    private void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        rb_Player.velocity = movement * f_Speed;

        if (horizontalInput == 0 && verticalInput == 0)
        {
            isRunning = false; // 달리기 애니메이션을 멈춤
        }
        else
        {
            isRunning = true; // 달리기 애니메이션을 재생
        }

        p_Ani.SetBool("p_Run", isRunning);
        PlayerDir();
    }

    private void PlayerDir()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            PlayerRenderer.flipX = true;
        }
        else
        {
            PlayerRenderer.flipX = false;
        }
    }

    void Attack()
    {
        if (Input.GetMouseButton(0))
        {
            p_Ani.SetTrigger("p_LeftAttack");
        }
        // else if (Input.GetMouseButtonDown(1))
        // {
        //     p_Ani.SetTrigger("p_LeftAttack");
        // }
    }
}