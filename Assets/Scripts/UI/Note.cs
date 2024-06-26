using UnityEngine;

public class Note : MonoBehaviour
{
    public GameObject notePanel; // ���� �г�
    private bool isPlayerInRange; // �÷��̾ ���� ���� �ִ��� Ȯ��

    void Update()
    {
        // �÷��̾ ���� ���� �ְ� FŰ�� ������ ��
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            ToggleNote(); // ���� ���� ǥ��/����� ���
        }
    }

    private void ToggleNote()
    {
        // �г� Ȱ��ȭ ���¸� ���
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
            notePanel.SetActive(false); // �÷��̾ ������ ��� �� �г� ��Ȱ��ȭ
        }
    }
}
