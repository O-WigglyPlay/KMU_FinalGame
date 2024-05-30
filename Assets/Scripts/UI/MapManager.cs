using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public GameObject mapUI; // 지도 UI 오브젝트
    public Image playerMarker; // 플레이어 마커 이미지
    public Transform player; // 플레이어 트랜스폼
    public float mapScale = 1.0f; // 월드 좌표를 맵 좌표로 변환하는 스케일
    public Vector2 mapOffset; // 월드 좌표에서 맵 좌표로 변환할 때 적용할 오프셋
    public RawImage mapRawImage; // 지도에 사용할 RawImage
    private Texture2D mapTexture; // 미니맵 텍스처

    private bool isMapActive = false; // 지도 활성화 상태

    void Start()
    {
        mapUI.SetActive(false); // 시작 시 지도 비활성화

        // 미니맵 텍스처 초기화
        mapTexture = new Texture2D(1024, 1024, TextureFormat.RGB24, false);
        for (int y = 0; y < mapTexture.height; y++)
        {
            for (int x = 0; x < mapTexture.width; x++)
            {
                mapTexture.SetPixel(x, y, Color.black); // 초기 색상 설정 (미방문 지역)
            }
        }
        mapTexture.Apply();

        // 지도 RawImage 설정
        mapRawImage.texture = mapTexture;

        // 플레이어 마커 크기 조정
        playerMarker.rectTransform.localScale = new Vector3(5.0f, 5.0f, 1.0f); // 원하는 크기로 조정
    }

    void Update()
    {
        // M 키를 누르면 지도 활성화/비활성화
        if (Input.GetKeyDown(KeyCode.M))
        {
            isMapActive = !isMapActive;
            mapUI.SetActive(isMapActive);
        }

        // 플레이어 마커 위치 업데이트
        if (isMapActive)
        {
            UpdatePlayerMarker();
        }

        // 플레이어 이동 경로 기록
        RecordPlayerPath();
    }

    void UpdatePlayerMarker()
    {
        // 플레이어 월드 좌표를 맵 좌표로 변환
        Vector3 playerPosition = player.position;
        Vector2 mapPosition = WorldToMapPosition(playerPosition);
        playerMarker.rectTransform.localPosition = new Vector3(mapPosition.x, mapPosition.y, 0);
    }

    void RecordPlayerPath()
    {
        // 플레이어 월드 좌표를 텍스처 좌표로 변환
        Vector3 playerPosition = player.position;
        Vector2 mapPosition = WorldToMapPosition(playerPosition);

        int x = (int)mapPosition.x;
        int y = (int)mapPosition.y;

        // 텍스처 범위 내에서만 기록
        if (x >= 0 && x < mapTexture.width && y >= 0 && y < mapTexture.height)
        {
            mapTexture.SetPixel(x, y, Color.white); // 방문한 지역을 흰색으로 설정
            mapTexture.Apply();
        }
    }

    Vector2 WorldToMapPosition(Vector3 worldPosition)
    {
        // 월드 좌표를 맵 좌표로 변환할 때 스케일과 오프셋 적용
        float mapX = (worldPosition.x + mapOffset.x) * mapScale + mapTexture.width / 2;
        float mapY = (worldPosition.y + mapOffset.y) * mapScale + mapTexture.height / 2;
        return new Vector2(mapX, mapY);
    }
}
