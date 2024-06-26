using UnityEngine;

public class Note : MonoBehaviour
{
    public GameObject notePanel; // 쪽지 패널
    private bool isPlayerInRange; // 플레이어가 범위 내에 있는지 확인

    void Update()
    {
        // 플레이어가 범위 내에 있고 F키를 눌렀을 때
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            ToggleNote(); // 쪽지 내용 표시/숨기기 토글
        }
    }

    private void ToggleNote()
    {
        // 패널 활성화 상태를 토글
        notePanel.SetActive(!notePanel.activeSelf);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            notePanel.SetActive(false); // 플레이어가 범위를 벗어날 때 패널 비활성화
        }
    }
}
