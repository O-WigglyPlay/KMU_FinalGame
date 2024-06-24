using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayerMove : MonoBehaviour
{
    public Player player; // �÷��̾� ��ũ��Ʈ ����
    public MapIcon mapIcon; // �� �������� MapIcon ��ũ��Ʈ ����

    public float checkInterval = 0.1f; // üũ ����
    public float stuckThreshold = 0.1f; // ���� ���θ� �Ǵ��� �Ÿ� �Ӱ谪

    private Vector2 oldPos;
    private float timer;

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player ������ �Ҵ���� �ʾҽ��ϴ�.");
            enabled = false;
            return;
        }

        if (mapIcon == null)
        {
            Debug.LogError("MapIcon ������ �Ҵ���� �ʾҽ��ϴ�.");
            enabled = false;
            return;
        }

        oldPos = player.transform.position;
        timer = checkInterval;
    }

    private void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;

        if (timer <= 0)
        {
            CheckPlayerMovement();
            timer = checkInterval; // Ÿ�̸� ����
        }
    }

    private void CheckPlayerMovement()
    {
        float distance = Vector2.Distance(player.transform.position, oldPos);

        // ������ �α� ���
        Debug.Log("Distance: " + distance + ", Is Colliding: " + player.IsColliding());

        // �÷��̾ ���߰ų� �浹 ������ �� �� �����ܵ� ����
        if (distance < stuckThreshold || player.IsColliding())
        {
            mapIcon.f_Speed = 0;
            mapIcon.SetMoveDirection(Vector2.zero);
        }
        else
        {
            mapIcon.f_Speed = player.f_Speed; // �÷��̾ �����̸� �� �����ܵ� �÷��̾��� �ӵ��� ������
            mapIcon.SetMoveDirection(player.GetLastMoveDirection());
        }

        oldPos = player.transform.position; // ���� ��ġ ������Ʈ
    }
}
