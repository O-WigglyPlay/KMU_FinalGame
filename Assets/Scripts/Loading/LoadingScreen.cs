using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OpenSettings()
    {
        // 설정 화면으로 이동하는 코드
    }
}
