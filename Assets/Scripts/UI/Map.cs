using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Transform playerPos; // �÷��̾� ��ġ
    public Transform offscreenPos; // ������ũ�� ��ġ
    public float speed; // �̵� �ӵ�

    private bool isMapVisible = false; // ������ ���� ���¸� �����ϴ� ����

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) // Ű�� ���� �� ���� ��ȯ
        {
            isMapVisible = !isMapVisible;
        }

        if (isMapVisible)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, offscreenPos.position, speed * Time.deltaTime);
        }
    }
}
