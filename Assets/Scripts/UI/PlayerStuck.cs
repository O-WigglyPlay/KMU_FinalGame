using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStuck : MonoBehaviour
{
    public float checkInterval = 0.1f; // 체크 간격
    public float stuckThreshold = 0.1f; // 정지 여부를 판단할 거리 임계값
    public Player player; // 플레이어 스크립트 참조
    public Transform mapIconTransform; // 맵 아이콘의 Transform 참조
    private Player mapIconPlayerScript; // 맵 아이콘의 Player 스크립트 참조

    private Vector2 oldPos;
    private float timer;

    private void Start()
    {
        if (player == null)
        {
            enabled = false;
            return;
        }

        if (mapIconTransform == null)
        {
            enabled = false;
            return;
        }

        // 맵 아이콘의 Player 스크립트 참조 설정
        mapIconPlayerScript = mapIconTransform.GetComponent<Player>();
        if (mapIconPlayerScript == null)
        {
            enabled = false;
            return;
        }

        oldPos = player.transform.position;
        timer = checkInterval;
    }

    private void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;

        if (timer <= 0)
        {
            CheckPlayerMovement();
            timer = checkInterval; // 타이머 리셋
        }
    }

    private void CheckPlayerMovement()
    {
        float distance = Vector2.Distance(player.transform.position, oldPos);

        // 플레이어가 멈추거나 충돌 상태일 때 맵 아이콘도 멈춤
        if (distance < stuckThreshold || player.IsColliding())
        {
            mapIconPlayerScript.f_Speed = 0;
        }
        else
        {
            mapIconPlayerScript.f_Speed = 2; // 플레이어가 움직이면 맵 아이콘도 움직임
        }

        oldPos = player.transform.position; // 이전 위치 업데이트
    }

}
