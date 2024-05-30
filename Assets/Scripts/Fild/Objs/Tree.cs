using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public int Tree_Hp = 10;  // 나무의 체력 초기화
    public GameObject teethPrefab;  // Teeth 프리팹에 대한 참조

    private void Update()
    {
        if (Tree_Hp <= 0)
        {
            Debug.Log("체력 0 이하 - 프리팹 생성 시도");

            if (teethPrefab != null)
            {
                Debug.Log("프리팹 존재 확인 - 생성 시작");
                Instantiate(teethPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);

                Debug.Log("프리팹 생성됨");
            }
            else
            {
                Debug.Log("프리팹 참조 없음");
            }
            Destroy(gameObject);
        }
    }
}