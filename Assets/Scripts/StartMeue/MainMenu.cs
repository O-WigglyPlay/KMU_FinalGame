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
        // ���� ȭ������ �̵��ϴ� �ڵ�
    }
}
