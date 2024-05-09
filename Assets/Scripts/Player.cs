using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int n_Hp;   //플레이어 체력
    public float f_Speed;   //플레이어 스피드
    private Rigidbody2D rb_Player;
    private void Start()
    {
        rb_Player = GetComponent<Rigidbody2D>();
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
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;

        // 입력에 따라 플레이어의 속도 설정
        rb_Player.velocity = movement * f_Speed;
    }
}
