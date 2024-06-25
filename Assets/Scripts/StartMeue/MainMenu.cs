using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public void OpenSettings()
    {
        // 설정 화면으로 이동하는 코드
    }
}
