using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [System.Serializable]
    public class SpawnableObjectGroup
    {
        public List<GameObject> prefabs;  // 생성할 프리팹 리스트
        public int count;                 // 생성할 오브젝트의 개수
        public float minDistance;         // 다른 오브젝트와의 최소 거리
    }

    [System.Serializable]
    public class SpawnArea
    {
        public Vector3 spawnAreaMin;  // 스폰 영역의 최소 값
        public Vector3 spawnAreaMax;  // 스폰 영역의 최대 값
        public List<SpawnableObjectGroup> objectGroupsToSpawn;  // 생성할 오브젝트 그룹 리스트
    }

    public List<SpawnArea> spawnAreas;  // 여러 스폰 영역 리스트

    private List<Vector3> spawnPositions = new List<Vector3>();  // 생성된 위치를 추적할 리스트

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

        // 유효한 위치를 찾기 위한 시도 횟수를 제한
        while (!validPosition && attempts < 100)
        {
            attempts++;
            // 랜덤한 위치 계산
            float randomX = Random.Range(area.spawnAreaMin.x, area.spawnAreaMax.x);
            float randomY = Random.Range(area.spawnAreaMin.y, area.spawnAreaMax.y);
            float randomZ = Random.Range(area.spawnAreaMin.z, area.spawnAreaMax.z);
            randomPosition = new Vector3(randomX, randomY, randomZ);

            // 다른 오브젝트와의 최소 거리 확인
            validPosition = true;
            foreach (Vector3 pos in spawnPositions)
            {
                if (Vector3.Distance(pos, randomPosition) < spawnableObjectGroup.minDistance)
                {
                    validPosition = false;
                    break;
                }
            }

            // 유효한 위치인 경우 리스트에 추가
            if (validPosition)
            {
                spawnPositions.Add(randomPosition);
                // 랜덤 프리팹 선택
                int prefabIndex = Random.Range(0, spawnableObjectGroup.prefabs.Count);
                GameObject prefabToSpawn = spawnableObjectGroup.prefabs[prefabIndex];
                GameObject newObject = Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);

                // 나무 프리팹일 경우 성장 애니메이션 시작
                Tree tree = newObject.GetComponent<Tree>();
                if (tree != null)
                {
                    tree.StartGrowthAnimation();
                }
            }
        }

<<<<<<< Updated upstream
        // 시도 횟수를 초과한 경우 (오브젝트를 생성할 위치를 찾지 못한 경우) 경고 메시지 출력 
        if (!validPosition)
        {
            Debug.LogWarning("Could not find a valid position to spawn an object from group.");
        }
=======
        // �õ� Ƚ���� �ʰ��� ��� (������Ʈ�� ������ ��ġ�� ã�� ���� ���) ��� �޽��� ���
        //if (!validPosition)
        //{
        //    Debug.LogWarning("Could not find a valid position to spawn an object from group.");
        //}
>>>>>>> Stashed changes
    }
}
