using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject[] animalPrefabs; // 생성할 동물 프리팹 배열
    public int[] numberOfAnimals; // 각 동물 프리팹마다 생성할 최대 개수 배열
    public BoxCollider2D spawnArea; // 스폰 영역을 정의하는 박스 콜라이더
    public BoxCollider2D despawnArea; // 디스폰 영역을 정의하는 박스 콜라이더

    private List<GameObject>[] spawnedAnimals; // 생성된 동물들을 추적하기 위한 리스트 배열

    void Start()
    {
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
        // 생성된 동물들 중 디스폰 영역을 벗어난 경우 제거
        for (int i = 0; i < spawnedAnimals.Length; i++)
        {
            List<GameObject> animals = spawnedAnimals[i];
            for (int j = 0; j < animals.Count; j++)
            {
                GameObject animal = animals[j];
                if (animal != null)
                {
                    if (!despawnArea.bounds.Contains(animal.transform.position))
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
            Vector3 spawnPosition = GetRandomSpawnPositionWithinArea(spawnArea);
            GameObject animal = Instantiate(animalPrefabs[index], spawnPosition, Quaternion.identity); // 동물 생성
            spawnedAnimals[index].Add(animal); // 생성된 동물을 리스트에 추가
            animal.layer = LayerMask.NameToLayer("Animal"); // 동물 레이어 설정
        }
    }

    // 박스 콜라이더 내에서 랜덤 위치를 반환하는 함수
    Vector3 GetRandomSpawnPositionWithinArea(BoxCollider2D area)
    {
        Bounds bounds = area.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector3(x, y, 0);
    }
}