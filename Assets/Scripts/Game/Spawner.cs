using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject ZombiePrefab;  // ���� ������
    public GameObject ghostPrefab;   // ���� ������
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

            // �����ϰ� ���� �Ǵ� ���� ����
            GameObject monsterPrefab = (Random.value > 0.5f) ? ZombiePrefab : ghostPrefab;

            // ���� ����
            GameObject Spawner = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);

            // ������ ���� Ȱ��ȭ (Ȥ�� ��Ȱ��ȭ�� ���·� �����Ǿ��ٸ�)
            Spawner.SetActive(true);

            // ������ �ð���ŭ ���
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}