using UnityEngine;
using UnityEngine.UI;

public class BuildingMenu : MonoBehaviour
{
    public GameObject[] buildingPrefabs;
    public GameObject buildingSystem;
    private BuildingSystem buildingSystemScript;

    void Start()
    {
        buildingSystemScript = buildingSystem.GetComponent<BuildingSystem>();
        buildingSystemScript.buildingPrefabs = buildingPrefabs; // BuildingSystem�� buildingPrefabs ����

        for (int i = 0; i < buildingPrefabs.Length; i++)
        {
            int index = i; // Capture the current value of i
            Button button = transform.GetChild(i).GetComponent<Button>();
            button.onClick.AddListener(() => {
                buildingSystemScript.SetBuildingPrefab(buildingPrefabs[index]);
                // ���� ��带 ���� �ʵ��� ToggleBuildingMode ȣ�� ����
            });
        }
    }

    void Update()
    {
        // 'B' Ű�� ������ ���� ���� ��带 ���
        if (Input.GetKeyDown(KeyCode.B))
        {
            buildingSystemScript.ToggleBuildingMode();
        }
    }
}
