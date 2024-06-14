using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject[] animalPrefabs; // ������ ���� ������ �迭
    public int maxAnimals; // ������ �ִ� ���� ��
    public Transform spawnPoint; // ������ ������ ���� ����Ʈ
    public float spawnInterval = 10f; // ������ ������ ���� (��)

    private List<GameObject> spawnedAnimals; // ������ �������� �����ϱ� ���� ����Ʈ
    private int currentAnimalCount = 0; // ���� ������ ���� ��

    void Start()
    {
        spawnedAnimals = new List<GameObject>(); // ������ �������� �����ϱ� ���� ����Ʈ �ʱ�ȭ
        StartCoroutine(SpawnAnimals()); // ���� ���� �ڷ�ƾ ����
    }

    void Update()
    {
        // ������ ������ �� null�� �������� ����Ʈ���� ����
        for (int i = spawnedAnimals.Count - 1; i >= 0; i--)
        {
            if (spawnedAnimals[i] == null)
            {
                spawnedAnimals.RemoveAt(i);
                currentAnimalCount--; // ���� ���� �� ����
                StartCoroutine(SpawnAnimals()); // ���ο� ���� ���� �ڷ�ƾ ����
            }
        }
    }

    // ������ �ֱ������� �����ϴ� �ڷ�ƾ
    IEnumerator SpawnAnimals()
    {
        while (currentAnimalCount < maxAnimals)
        {
            yield return new WaitForSeconds(spawnInterval); // ���� ���ݸ�ŭ ���

            if (currentAnimalCount < maxAnimals)
            {
                SpawnAnimal(); // ���� ����
            }
        }
    }

    // ���� ���� �Լ�
    void SpawnAnimal()
    {
        int randomIndex = Random.Range(0, animalPrefabs.Length); // ������ ���� ������ ����
        GameObject animal = Instantiate(animalPrefabs[randomIndex], spawnPoint.position, Quaternion.identity); // ���� ����
        spawnedAnimals.Add(animal); // ������ ������ ����Ʈ�� �߰�
        currentAnimalCount++; // ���� ���� �� ����
    }
}