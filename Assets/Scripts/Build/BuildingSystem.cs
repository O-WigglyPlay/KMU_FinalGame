using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public GameObject[] buildingPrefabs;
    public GameObject[] previewPrefabs;
    private GameObject currentPreviewObject;
    private GameObject currentBuildingPrefab;
    public LayerMask collisionLayerMask; // 충돌을 감지할 레이어 마스크
    public Vector2 buildingSize = new Vector2(2.0f, 2.0f); // 실제 충돌 박스 크기
    public Vector2 collisionCheckSize = new Vector2(2.5f, 2.5f); // 충돌을 감지할 박스 크기
    private bool isBuildingMode = false;
    public GameObject buildingMenuUI; // 건축 메뉴 UI 패널 참조
    private float currentRotation = 0f; // 현재 회전 각도

    void Start()
    {
        buildingMenuUI.SetActive(false); // 시작할 때 UI 비활성화
    }

    void Update()
    {
        if (isBuildingMode)
        {
            if (currentPreviewObject != null)
            {
                Vector2 buildPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                currentPreviewObject.transform.position = buildPosition;

                if (CanBuildAtPosition(buildPosition))
                {
                    SetPreviewColor(Color.green);
                }
                else
                {
                    SetPreviewColor(Color.red);
                }

                // 마우스 휠을 통해 회전 각도 변경
                if (Input.mouseScrollDelta.y != 0)
                {
                    currentRotation += Input.mouseScrollDelta.y * 15f; // 회전 각도를 15도씩 변경
                    currentPreviewObject.transform.rotation = Quaternion.Euler(0, 0, currentRotation);
                }

                if (Input.GetMouseButtonDown(0) && CanBuildAtPosition(buildPosition))
                {
                    Instantiate(currentBuildingPrefab, buildPosition, Quaternion.Euler(0, 0, currentRotation));
                }
            }

            if (Input.GetKeyDown(KeyCode.B)) // Toggle building mode with 'B' key
            {
                ToggleBuildingMode();
            }
        }
        else if (Input.GetKeyDown(KeyCode.B)) // Allow re-entering building mode
        {
            ToggleBuildingMode();
        }
    }

    public void SetBuildingPrefab(GameObject prefab)
    {
        currentBuildingPrefab = prefab;
        if (currentPreviewObject != null)
        {
            Destroy(currentPreviewObject);
        }
        int prefabIndex = System.Array.IndexOf(buildingPrefabs, prefab);
        if (prefabIndex >= 0 && prefabIndex < previewPrefabs.Length)
        {
            currentPreviewObject = Instantiate(previewPrefabs[prefabIndex], Vector3.zero, Quaternion.identity);
            currentRotation = 0f; // 새로운 미리보기 오브젝트를 생성할 때 회전 각도를 초기화
        }
    }

    public void ToggleBuildingMode()
    {
        isBuildingMode = !isBuildingMode;
        buildingMenuUI.SetActive(isBuildingMode); // UI 패널 활성화/비활성화

        if (!isBuildingMode && currentPreviewObject != null)
        {
            Destroy(currentPreviewObject);
            currentPreviewObject = null;
        }
    }

    bool CanBuildAtPosition(Vector2 position)
    {
        // 충돌 박스의 크기를 조정하여 충돌 범위를 넓힘
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(position, collisionCheckSize, 0f, collisionLayerMask);
        return hitColliders.Length == 0;
    }

    void SetPreviewColor(Color color)
    {
        SpriteRenderer renderer = currentPreviewObject.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.color = color;
        }
    }
}
