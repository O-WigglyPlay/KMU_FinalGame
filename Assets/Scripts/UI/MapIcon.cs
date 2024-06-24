using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapIcon : MonoBehaviour
{
    public float f_Speed; // �� �������� �ӵ�
    private Vector2 moveDirection; // �̵� ����

    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction;
    }

    private void Update()
    {
        // ����� �ӵ��� ���� �� �������� �̵���Ŵ
        transform.Translate(moveDirection * f_Speed * Time.deltaTime);

        // ������ �α� ���
        Debug.Log("Move Direction: " + moveDirection + ", Speed: " + f_Speed);
    }
}
