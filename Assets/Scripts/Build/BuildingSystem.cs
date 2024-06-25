using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public GameObject[] buildingPrefabs;
    public GameObject[] previewPrefabs;
    private GameObject currentPreviewObject;
    private GameObject currentBuildingPrefab;
    public LayerMask collisionLayerMask; // �浹�� ������ ���̾� ����ũ
    public Vector2 buildingSize = new Vector2(2.0f, 2.0f); // ���� �浹 �ڽ� ũ��
    public Vector2 collisionCheckSize = new Vector2(2.5f, 2.5f); // �浹�� ������ �ڽ� ũ��
    private bool isBuildingMode = false;
    public GameObject buildingMenuUI; // ���� �޴� UI �г� ����
    private float currentRotation = 0f; // ���� ȸ�� ����

    void Start()
    {
        buildingMenuUI.SetActive(false); // ������ �� UI ��Ȱ��ȭ
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

                // ���콺 ���� ���� ȸ�� ���� ����
                if (Input.mouseScrollDelta.y != 0)
                {
                    currentRotation += Input.mouseScrollDelta.y * 15f; // ȸ�� ������ 15���� ����
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
            currentRotation = 0f; // ���ο� �̸����� ������Ʈ�� ������ �� ȸ�� ������ �ʱ�ȭ
        }
    }

    public void ToggleBuildingMode()
    {
        isBuildingMode = !isBuildingMode;
        buildingMenuUI.SetActive(isBuildingMode); // UI �г� Ȱ��ȭ/��Ȱ��ȭ

        if (!isBuildingMode && currentPreviewObject != null)
        {
            Destroy(currentPreviewObject);
            currentPreviewObject = null;
        }
    }

    bool CanBuildAtPosition(Vector2 position)
    {
        // �浹 �ڽ��� ũ�⸦ �����Ͽ� �浹 ������ ����
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
