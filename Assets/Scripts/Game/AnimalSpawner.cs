using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject[] animalPrefabs; // 생성할 동물 프리팹 배열
    public int maxAnimals; // 생성할 최대 동물 수
    public Transform spawnPoint; // 동물이 생성될 스폰 포인트
    public float spawnInterval = 10f; // 동물을 생성할 간격 (초)

    private List<GameObject> spawnedAnimals; // 생성된 동물들을 추적하기 위한 리스트
    private int currentAnimalCount = 0; // 현재 생성된 동물 수

    void Start()
    {
        spawnedAnimals = new List<GameObject>(); // 생성된 동물들을 추적하기 위한 리스트 초기화
        StartCoroutine(SpawnAnimals()); // 동물 생성 코루틴 시작
    }

    void Update()
    {
        // 생성된 동물들 중 null인 동물들을 리스트에서 제거
        for (int i = spawnedAnimals.Count - 1; i >= 0; i--)
        {
            if (spawnedAnimals[i] == null)
            {
                spawnedAnimals.RemoveAt(i);
                currentAnimalCount--; // 현재 동물 수 감소
                StartCoroutine(SpawnAnimals()); // 새로운 동물 생성 코루틴 시작
            }
        }
    }

    // 동물을 주기적으로 생성하는 코루틴
    IEnumerator SpawnAnimals()
    {
        while (currentAnimalCount < maxAnimals)
        {
            yield return new WaitForSeconds(spawnInterval); // 스폰 간격만큼 대기

            if (currentAnimalCount < maxAnimals)
            {
                SpawnAnimal(); // 동물 생성
            }
        }
    }

    // 동물 생성 함수
    void SpawnAnimal()
    {
        int randomIndex = Random.Range(0, animalPrefabs.Length); // 랜덤한 동물 프리팹 선택
        GameObject animal = Instantiate(animalPrefabs[randomIndex], spawnPoint.position, Quaternion.identity); // 동물 생성
        spawnedAnimals.Add(animal); // 생성된 동물을 리스트에 추가
        currentAnimalCount++; // 현재 동물 수 증가
    }
}