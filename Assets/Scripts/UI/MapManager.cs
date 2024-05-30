using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public GameObject mapUI; // ���� UI ������Ʈ
    public Image playerMarker; // �÷��̾� ��Ŀ �̹���
    public Transform player; // �÷��̾� Ʈ������
    public float mapScale = 1.0f; // ���� ��ǥ�� �� ��ǥ�� ��ȯ�ϴ� ������
    public Vector2 mapOffset; // ���� ��ǥ���� �� ��ǥ�� ��ȯ�� �� ������ ������
    public RawImage mapRawImage; // ������ ����� RawImage
    private Texture2D mapTexture; // �̴ϸ� �ؽ�ó

    private bool isMapActive = false; // ���� Ȱ��ȭ ����

    void Start()
    {
        mapUI.SetActive(false); // ���� �� ���� ��Ȱ��ȭ

        // �̴ϸ� �ؽ�ó �ʱ�ȭ
        mapTexture = new Texture2D(1024, 1024, TextureFormat.RGB24, false);
        for (int y = 0; y < mapTexture.height; y++)
        {
            for (int x = 0; x < mapTexture.width; x++)
            {
                mapTexture.SetPixel(x, y, Color.black); // �ʱ� ���� ���� (�̹湮 ����)
            }
        }
        mapTexture.Apply();

        // ���� RawImage ����
        mapRawImage.texture = mapTexture;

        // �÷��̾� ��Ŀ ũ�� ����
        playerMarker.rectTransform.localScale = new Vector3(5.0f, 5.0f, 1.0f); // ���ϴ� ũ��� ����
    }

    void Update()
    {
        // M Ű�� ������ ���� Ȱ��ȭ/��Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.M))
        {
            isMapActive = !isMapActive;
            mapUI.SetActive(isMapActive);
        }

        // �÷��̾� ��Ŀ ��ġ ������Ʈ
        if (isMapActive)
        {
            UpdatePlayerMarker();
        }

        // �÷��̾� �̵� ��� ���
        RecordPlayerPath();
    }

    void UpdatePlayerMarker()
    {
        // �÷��̾� ���� ��ǥ�� �� ��ǥ�� ��ȯ
        Vector3 playerPosition = player.position;
        Vector2 mapPosition = WorldToMapPosition(playerPosition);
        playerMarker.rectTransform.localPosition = new Vector3(mapPosition.x, mapPosition.y, 0);
    }

    void RecordPlayerPath()
    {
        // �÷��̾� ���� ��ǥ�� �ؽ�ó ��ǥ�� ��ȯ
        Vector3 playerPosition = player.position;
        Vector2 mapPosition = WorldToMapPosition(playerPosition);

        int x = (int)mapPosition.x;
        int y = (int)mapPosition.y;

        // �ؽ�ó ���� �������� ���
        if (x >= 0 && x < mapTexture.width && y >= 0 && y < mapTexture.height)
        {
            mapTexture.SetPixel(x, y, Color.white); // �湮�� ������ ������� ����
            mapTexture.Apply();
        }
    }

    Vector2 WorldToMapPosition(Vector3 worldPosition)
    {
        // ���� ��ǥ�� �� ��ǥ�� ��ȯ�� �� �����ϰ� ������ ����
        float mapX = (worldPosition.x + mapOffset.x) * mapScale + mapTexture.width / 2;
        float mapY = (worldPosition.y + mapOffset.y) * mapScale + mapTexture.height / 2;
        return new Vector2(mapX, mapY);
    }
}
