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
        buildingSystemScript.buildingPrefabs = buildingPrefabs; // BuildingSystem¿¡ buildingPrefabs Àü´Þ

        for (int i = 0; i < buildingPrefabs.Length; i++)
        {
            int index = i; // Capture the current value of i
            Button button = transform.GetChild(i).GetComponent<Button>();
            button.onClick.AddListener(() => {
                buildingSystemScript.SetBuildingPrefab(buildingPrefabs[index]);
                buildingSystemScript.ToggleBuildingMode(); // Enable building mode when a prefab is selected
            });
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            buildingSystemScript.ToggleBuildingMode();
        }
    }
}
