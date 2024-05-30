using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public int Tree_Hp = 10;  // 나무의 체력 초기화
    public GameObject teethPrefab;  // Teeth 프리팹에 대한 참조
    private bool isDestroyed = false;  // 프리팹이 생성되었는지 여부를 추적하는 플래그

    private void Update()
    {
        if (Tree_Hp <= 0 && !isDestroyed)
        {
            Debug.Log("체력 0 이하 - 프리팹 생성 시도");

            if (teethPrefab != null)
            {
                Debug.Log("프리팹 존재 확인 - 생성 시작");
                Instantiate(teethPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                Debug.Log("프리팹 생성됨");
                isDestroyed = true;  // 프리팹이 생성되었음을 표시
            }
            else
            {
                Debug.Log("프리팹 참조 없음");
            }
            StartCoroutine(DestroyTree());
        }
    }

    private IEnumerator DestroyTree()
    {
        // 0.1초의 지연 시간 후에 나무 오브젝트를 삭제
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}