using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject[] animalPrefabs; // ������ ���� ������ �迭
    public int[] numberOfAnimals; // �� ���� �����ո��� ������ �ִ� ���� �迭
    public float spawnRadius = 10f; // �÷��̾� �ֺ��� ������ �ݰ�
    public float despawnDistance = 20f; // �÷��̾�κ��� �� �Ÿ� �̻� ������ ������ �����

    private List<GameObject>[] spawnedAnimals; // ������ �������� �����ϱ� ���� ����Ʈ �迭
    private GameObject player; // �÷��̾�


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // �÷��̾� ã��

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
        // ������ ������ �� �÷��̾���� �Ÿ��� Ȯ���Ͽ� ���� �Ÿ� �̻� �־����� ����
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
            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius; // �÷��̾� �ֺ��� ������ ��ġ ���� ����
            Vector3 spawnPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0); // �÷��̾� ��ġ�� ���� ��ġ�� ���Ͽ� ���� ��ġ ���
            GameObject animal = Instantiate(animalPrefabs[index], spawnPosition, Quaternion.identity); // ���� ����
            spawnedAnimals[index].Add(animal); // ������ ������ ����Ʈ�� �߰�
        }
    }
}