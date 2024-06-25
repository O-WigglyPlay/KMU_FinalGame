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
    public int maxMonsters = 8; // 최대 몬스터 수

    public float fieldMinX = -10f; // 필드의 최소 X 좌표
    public float fieldMaxX = 10f; // 필드의 최대 X 좌표
    public float fieldMinY = -10f; // 필드의 최소 Y 좌표
    public float fieldMaxY = 10f; // 필드의 최대 Y 좌표

    private List<GameObject> spawnedMonsters = new List<GameObject>(); // 생성된 몬스터 리스트

    private void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    private IEnumerator SpawnMonsters()
    {
        while (true)
        {
            // 활성화된 몬스터 수를 갱신
            spawnedMonsters.RemoveAll(monster => monster == null);

            if (spawnedMonsters.Count < maxMonsters)
            {
                Vector2 spawnPosition;

                // 유효한 위치를 찾을 때까지 반복
                do
                {
                    // 랜덤한 각도를 생성
                    float angle = Random.Range(0, 2 * Mathf.PI);
                    // 랜덤한 반경 거리를 생성
                    float radius = Random.Range(0, spawnRadius);
                    // 랜덤 위치 계산
                    spawnPosition = new Vector2(
                        playerTransform.position.x + Mathf.Cos(angle) * radius,
                        playerTransform.position.y + Mathf.Sin(angle) * radius
                    );
                } while (!IsWithinFieldBounds(spawnPosition));

                // 랜덤하게 몬스터 선택
                GameObject monsterPrefab = GetRandomMonsterPrefab();

                // 몬스터 생성
                GameObject spawnedMonster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);

                // 생성된 몬스터 리스트에 추가
                spawnedMonsters.Add(spawnedMonster);

                // 생성된 몬스터 활성화 (혹시 비활성화된 상태로 생성되었다면)
                spawnedMonster.SetActive(true);
            }

            // 지정된 시간만큼 대기
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private bool IsWithinFieldBounds(Vector2 position)
    {
        // 위치가 필드 경계 내에 있는지 확인
        return position.x >= fieldMinX && position.x <= fieldMaxX && position.y >= fieldMinY && position.y <= fieldMaxY;
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