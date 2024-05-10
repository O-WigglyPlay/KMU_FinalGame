using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int n_Hp;   // 플레이어 체력
    public float f_Speed;   // 플레이어 스피드
    private bool isRunning = false;
    private Rigidbody2D rb_Player;
    private Animator p_Ani;

    private void Start()
    {
        rb_Player = GetComponent<Rigidbody2D>();
        p_Ani = GetComponent<Animator>();
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
        p_Ani.SetBool("p_Run", isRunning);
        Debug.Log(isRunning);
        if (horizontalInput == 0 & verticalInput == 0)
        {
            isRunning = false; // 달리기 애니메이션을 멈춤
        }
        else
        {
            isRunning = true; // 달리기 애니메이션을 재생
        }
    }
}