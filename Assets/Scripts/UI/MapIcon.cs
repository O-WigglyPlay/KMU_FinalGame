using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapIcon : MonoBehaviour
{
    public float f_Speed; // 맵 아이콘의 속도
    private Vector2 moveDirection; // 이동 방향

    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction;
    }

    private void Update()
    {
        // 방향과 속도에 따라 맵 아이콘을 이동시킴
        transform.Translate(moveDirection * f_Speed * Time.deltaTime);

        // 디버깅용 로그 출력
        Debug.Log("Move Direction: " + moveDirection + ", Speed: " + f_Speed);
    }
}
