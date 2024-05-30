using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject ZombiePrefab;  // 좀비 프리팹
    public GameObject BigghostPrefab;   // 큰유령 프리팹
    public GameObject SmallGhostPrefab; // 작은 유령 프리팹
    public GameObject SpliterZombiePrefab; // 원거리 좀비 프리팹

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

            // 랜덤하게 몬스터 선택
            GameObject monsterPrefab = GetRandomMonsterPrefab();

            // 몬스터 생성
            GameObject spawnedMonster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);

            // 생성된 몬스터 활성화 (혹시 비활성화된 상태로 생성되었다면)
            spawnedMonster.SetActive(true);

            // 지정된 시간만큼 대기
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private GameObject GetRandomMonsterPrefab()
    {
        int randomIndex = Random.Range(0, 4); // 0에서 3까지의 랜덤 정수 생성
        switch (randomIndex)
        {
            case 0:
                return ZombiePrefab;
            case 1:
                return BigghostPrefab;
            case 2:
                return SmallGhostPrefab;
            case 3:
                return SpliterZombiePrefab;
            default:
                return ZombiePrefab; // 기본값
        }
    }
}