using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [System.Serializable]
    public class SpawnableObjectGroup
    {
        public List<GameObject> prefabs;  // ������ ������ ����Ʈ
        public int count;                 // ������ ������Ʈ�� ����
        public float minDistance;         // �ٸ� ������Ʈ���� �ּ� �Ÿ�
    }

    [System.Serializable]
    public class SpawnArea
    {
        public Vector3 spawnAreaMin;  // ���� ������ �ּ� ��
        public Vector3 spawnAreaMax;  // ���� ������ �ִ� ��
        public List<SpawnableObjectGroup> objectGroupsToSpawn;  // ������ ������Ʈ �׷� ����Ʈ
    }

    public List<SpawnArea> spawnAreas;  // ���� ���� ���� ����Ʈ

    private List<Vector3> spawnPositions = new List<Vector3>();  // ������ ��ġ�� ������ ����Ʈ

    void Start()
    {
        foreach (SpawnArea area in spawnAreas)
        {
            foreach (SpawnableObjectGroup group in area.objectGroupsToSpawn)
            {
                for (int i = 0; i < group.count; i++)
                {
                    SpawnObject(group, area);
                }
            }
        }
    }

    void SpawnObject(SpawnableObjectGroup spawnableObjectGroup, SpawnArea area)
    {
        Vector3 randomPosition;
        int attempts = 0;
        bool validPosition = false;

        // ��ȿ�� ��ġ�� ã�� ���� �õ� Ƚ���� ����
        while (!validPosition && attempts < 100)
        {
            attempts++;
            // ������ ��ġ ���
            float randomX = Random.Range(area.spawnAreaMin.x, area.spawnAreaMax.x);
            float randomY = Random.Range(area.spawnAreaMin.y, area.spawnAreaMax.y);
            float randomZ = Random.Range(area.spawnAreaMin.z, area.spawnAreaMax.z);
            randomPosition = new Vector3(randomX, randomY, randomZ);

            // �ٸ� ������Ʈ���� �ּ� �Ÿ� Ȯ��
            validPosition = true;
            foreach (Vector3 pos in spawnPositions)
            {
                if (Vector3.Distance(pos, randomPosition) < spawnableObjectGroup.minDistance)
                {
                    validPosition = false;
                    break;
                }
            }

            // ��ȿ�� ��ġ�� ��� ����Ʈ�� �߰�
            if (validPosition)
            {
                spawnPositions.Add(randomPosition);
                // ���� ������ ����
                int prefabIndex = Random.Range(0, spawnableObjectGroup.prefabs.Count);
                GameObject prefabToSpawn = spawnableObjectGroup.prefabs[prefabIndex];
                GameObject newObject = Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);

                // ���� �������� ��� ���� �ִϸ��̼� ����
                Tree tree = newObject.GetComponent<Tree>();
                if (tree != null)
                {
                    tree.StartGrowthAnimation();
                }
            }
        }

        // �õ� Ƚ���� �ʰ��� ��� (������Ʈ�� ������ ��ġ�� ã�� ���� ���) ��� �޽��� ���
        if (!validPosition)
        {
            Debug.LogWarning("Could not find a valid position to spawn an object from group.");
        }
    }
}
