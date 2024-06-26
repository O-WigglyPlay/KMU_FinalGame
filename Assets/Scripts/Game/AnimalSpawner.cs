using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject[] animalPrefabs; // ������ ���� ������ �迭
    public int[] numberOfAnimals; // �� ���� �����ո��� ������ �ִ� ���� �迭
    public BoxCollider2D spawnArea; // ���� ������ �����ϴ� �ڽ� �ݶ��̴�
    public BoxCollider2D despawnArea; // ���� ������ �����ϴ� �ڽ� �ݶ��̴�

    private List<GameObject>[] spawnedAnimals; // ������ �������� �����ϱ� ���� ����Ʈ �迭

    void Start()
    {
        // ������ �������� �����ϱ� ���� ����Ʈ �迭 �ʱ�ȭ
        spawnedAnimals = new List<GameObject>[animalPrefabs.Length];
        for (int i = 0; i < spawnedAnimals.Length; i++)
        {
            spawnedAnimals[i] = new List<GameObject>();
        }

        // �ʱ⿡ ���� ����
        for (int i = 0; i < animalPrefabs.Length; i++)
        {
            SpawnAnimals(i, numberOfAnimals[i]);
        }
    }

    void Update()
    {
        // ������ ������ �� ���� ������ ��� ��� ����
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
                        j--; // ����Ʈ���� ��Ұ� ���ŵǾ����Ƿ� �ε��� ����
                        // ���ο� ���� ����
                        SpawnAnimals(i, 1);
                    }
                }
                else // ������ null�� ���, ����Ʈ���� �����ϰ� ���ο� ���� ����
                {
                    animals.RemoveAt(j);
                    j--; // ����Ʈ���� ��Ұ� ���ŵǾ����Ƿ� �ε��� ����
                    SpawnAnimals(i, 1);
                }
            }
        }
    }

    // ���� ���� �Լ�
    void SpawnAnimals(int index, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPositionWithinArea(spawnArea);
            GameObject animal = Instantiate(animalPrefabs[index], spawnPosition, Quaternion.identity); // ���� ����
            spawnedAnimals[index].Add(animal); // ������ ������ ����Ʈ�� �߰�
            animal.layer = LayerMask.NameToLayer("Animal"); // ���� ���̾� ����
        }
    }

    // �ڽ� �ݶ��̴� ������ ���� ��ġ�� ��ȯ�ϴ� �Լ�
    Vector3 GetRandomSpawnPositionWithinArea(BoxCollider2D area)
    {
        Bounds bounds = area.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector3(x, y, 0);
    }
}