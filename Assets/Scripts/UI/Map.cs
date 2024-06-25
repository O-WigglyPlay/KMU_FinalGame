using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Transform playerPos; // 플레이어 위치
    public Transform offscreenPos; // 오프스크린 위치
    public float speed; // 이동 속도

    private bool isMapVisible = false; // 지도의 현재 상태를 저장하는 변수

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) // 키가 눌릴 때 상태 전환
        {
            isMapVisible = !isMapVisible;
        }

        if (isMapVisible)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, offscreenPos.position, speed * Time.deltaTime);
        }
    }
}
