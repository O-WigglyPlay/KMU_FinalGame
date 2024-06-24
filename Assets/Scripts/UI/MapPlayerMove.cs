using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayerMove : MonoBehaviour
{
    public Player player; // 플레이어 스크립트 참조
    public MapIcon mapIcon; // 맵 아이콘의 MapIcon 스크립트 참조

    public float checkInterval = 0.1f; // 체크 간격
    public float stuckThreshold = 0.1f; // 정지 여부를 판단할 거리 임계값

    private Vector2 oldPos;
    private float timer;

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player 참조가 할당되지 않았습니다.");
            enabled = false;
            return;
        }

        if (mapIcon == null)
        {
            Debug.LogError("MapIcon 참조가 할당되지 않았습니다.");
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

        // 디버깅용 로그 출력
        Debug.Log("Distance: " + distance + ", Is Colliding: " + player.IsColliding());

        // 플레이어가 멈추거나 충돌 상태일 때 맵 아이콘도 멈춤
        if (distance < stuckThreshold || player.IsColliding())
        {
            mapIcon.f_Speed = 0;
            mapIcon.SetMoveDirection(Vector2.zero);
        }
        else
        {
            mapIcon.f_Speed = player.f_Speed; // 플레이어가 움직이면 맵 아이콘도 플레이어의 속도로 움직임
            mapIcon.SetMoveDirection(player.GetLastMoveDirection());
        }

        oldPos = player.transform.position; // 이전 위치 업데이트
    }
}
