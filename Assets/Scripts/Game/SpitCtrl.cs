using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitCtrl : MonoBehaviour
{
    public float speed = 5f; // ����ü�� �ӵ�
    private Vector2 targetDirection;

    // ����ü�� �̵� ���� ����
    public void SetDirection(Vector2 direction)
    {
        targetDirection = direction.normalized;
    }

    void Update()
    {
        // ����ü �̵�
        transform.Translate(targetDirection * speed * Time.deltaTime);

        // ���� �ð��� ������ ����ü�� �ı� (�ʿ信 ���� ���� ����)
        Destroy(gameObject, 5f);
    }

    // �浹 ó�� (�ʿ信 ���� ���� ����)
    void OnCollisionEnter2D(Collision2D collision)
    {
        // ���⼭ �浹 ���� �߰�
        // ��: �÷��̾�� �浹 �� ������ ó�� ��
        Destroy(gameObject); // �浹 �� ����ü �ı�
    }
}
