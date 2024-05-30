using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject[] animalPrefabs; // 생성할 동물 프리팹 배열
    public int[] numberOfAnimals; // 각 동물 프리팹마다 생성할 최대 개수 배열
    public float spawnRadius = 10f; // 플레이어 주변에 생성할 반경
    public float despawnDistance = 20f; // 플레이어로부터 이 거리 이상 떨어진 동물은 사라짐

    private List<GameObject>[] spawnedAnimals; // 생성된 동물들을 추적하기 위한 리스트 배열
    private GameObject player; // 플레이어


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // 플레이어 찾기

        // 생성된 동물들을 추적하기 위한 리스트 배열 초기화
        spawnedAnimals = new List<GameObject>[animalPrefabs.Length];
        for (int i = 0; i < spawnedAnimals.Length; i++)
        {
            spawnedAnimals[i] = new List<GameObject>();
        }

        // 초기에 동물 생성
        for (int i = 0; i < animalPrefabs.Length; i++)
        {
            SpawnAnimals(i, numberOfAnimals[i]);
        }
    }

    void Update()
    {
        // 생성된 동물들 중 플레이어와의 거리를 확인하여 일정 거리 이상 멀어지면 제거
        for (int i = 0; i < spawnedAnimals.Length; i++)
        {
            List<GameObject> animals = spawnedAnimals[i];
            for (int j = 0; j < animals.Count; j++)
            {
                GameObject animal = animals[j];
                if (animal != null)
                {
                    float distanceToPlayer = Vector3.Distance(animal.transform.position, player.transform.position);
                    if (distanceToPlayer > despawnDistance)
                    {
                        Destroy(animal);
                        animals.RemoveAt(j);
                        j--; // 리스트에서 요소가 제거되었으므로 인덱스 조정
                        // 새로운 동물 생성
                        SpawnAnimals(i, 1);
                    }
                }
                else // 동물이 null일 경우, 리스트에서 제거하고 새로운 동물 생성
                {
                    animals.RemoveAt(j);
                    j--; // 리스트에서 요소가 제거되었으므로 인덱스 조정
                    SpawnAnimals(i, 1);
                }
            }
        }
    }

    // 동물 생성 함수
    void SpawnAnimals(int index, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius; // 플레이어 주변에 랜덤한 위치 벡터 생성
            Vector3 spawnPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0); // 플레이어 위치에 랜덤 위치를 더하여 생성 위치 계산
            GameObject animal = Instantiate(animalPrefabs[index], spawnPosition, Quaternion.identity); // 동물 생성
            spawnedAnimals[index].Add(animal); // 생성된 동물을 리스트에 추가
        }
    }
}