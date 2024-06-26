using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static string filePath;

    void Awake()
    {
        // 파일 경로 설정
        filePath = Path.Combine(Application.persistentDataPath, "gamedata.json");
    }

    // 게임 데이터를 JSON 파일로 저장하는 메서드
    public static void SaveGameData(GameData data)
    {
        // 객체를 JSON 형식으로 변환
        string json = JsonUtility.ToJson(data, true);
        // JSON 데이터를 파일에 저장
        File.WriteAllText(filePath, json);
    }

    // JSON 파일에서 게임 데이터를 불러오는 메서드
    public static GameData LoadGameData()
    {
        // 파일이 존재하는지 확인
        if (File.Exists(filePath))
        {
            // 파일에서 JSON 데이터 읽기
            string json = File.ReadAllText(filePath);
            // JSON 데이터를 객체로 변환
            GameData data = JsonUtility.FromJson<GameData>(json);
            return data;
        }
        else
        {
            return new GameData();
        }
    }
}