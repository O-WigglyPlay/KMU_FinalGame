using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject ZombiePrefab;  // 좀비 프리팹
    public GameObject ghostPrefab;   // 유령 프리팹
    public float spawnInterval = 3f; // 몬스터 생성 간격
    public float spawnRadius = 5f;   // 몬스터 생성 반경
    public Transform playerTransform; // 플레이어의 Transform

    private void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    private IEnumerator SpawnMonsters()
    {
        while (true)
        {
            // 랜덤한 각도를 생성
            float angle = Random.Range(0, 2 * Mathf.PI);
            // 랜덤한 반경 거리를 생성
            float radius = Random.Range(0, spawnRadius);
            // 랜덤 위치 계산
            Vector2 spawnPosition = new Vector2(
                playerTransform.position.x + Mathf.Cos(angle) * radius,
                playerTransform.position.y + Mathf.Sin(angle) * radius
            );

            // 랜덤하게 좀비 또는 유령 선택
            GameObject monsterPrefab = (Random.value > 0.5f) ? ZombiePrefab : ghostPrefab;

            // 몬스터 생성
            GameObject Spawner = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);

            // 생성된 몬스터 활성화 (혹시 비활성화된 상태로 생성되었다면)
            Spawner.SetActive(true);

            // 지정된 시간만큼 대기
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}