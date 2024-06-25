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
        buildingSystemScript.buildingPrefabs = buildingPrefabs; // BuildingSystem에 buildingPrefabs 전달

        for (int i = 0; i < buildingPrefabs.Length; i++)
        {
            int index = i; // Capture the current value of i
            Button button = transform.GetChild(i).GetComponent<Button>();
            button.onClick.AddListener(() => {
                buildingSystemScript.SetBuildingPrefab(buildingPrefabs[index]);
                // 건축 모드를 끄지 않도록 ToggleBuildingMode 호출 제거
            });
        }
    }

    void Update()
    {
        // 'B' 키를 눌렀을 때만 건축 모드를 토글
        if (Input.GetKeyDown(KeyCode.B))
        {
            buildingSystemScript.ToggleBuildingMode();
        }
    }
}
