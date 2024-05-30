using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject ZombiePrefab;  // ���� ������
    public GameObject BigghostPrefab;   // ū���� ������
    public GameObject SmallGhostPrefab; // ���� ���� ������
    public GameObject SpliterZombiePrefab; // ���Ÿ� ���� ������

    public float spawnInterval = 3f; // ���� ���� ����
    public float spawnRadius = 5f;   // ���� ���� �ݰ�
    public Transform playerTransform; // �÷��̾��� Transform

    private void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    private IEnumerator SpawnMonsters()
    {
        while (true)
        {
            // ������ ������ ����
            float angle = Random.Range(0, 2 * Mathf.PI);
            // ������ �ݰ� �Ÿ��� ����
            float radius = Random.Range(0, spawnRadius);
            // ���� ��ġ ���
            Vector2 spawnPosition = new Vector2(
                playerTransform.position.x + Mathf.Cos(angle) * radius,
                playerTransform.position.y + Mathf.Sin(angle) * radius
            );

            // �����ϰ� ���� ����
            GameObject monsterPrefab = GetRandomMonsterPrefab();

            // ���� ����
            GameObject spawnedMonster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);

            // ������ ���� Ȱ��ȭ (Ȥ�� ��Ȱ��ȭ�� ���·� �����Ǿ��ٸ�)
            spawnedMonster.SetActive(true);

            // ������ �ð���ŭ ���
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private GameObject GetRandomMonsterPrefab()
    {
        int randomIndex = Random.Range(0, 4); // 0���� 3������ ���� ���� ����
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
                return ZombiePrefab; // �⺻��
        }
    }
}