using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStuck : MonoBehaviour
{
    public float checkInterval = 0.1f; // üũ ����
    public float stuckThreshold = 0.1f; // ���� ���θ� �Ǵ��� �Ÿ� �Ӱ谪
    public Player player; // �÷��̾� ��ũ��Ʈ ����
    public Transform mapIconTransform; // �� �������� Transform ����
    private Player mapIconPlayerScript; // �� �������� Player ��ũ��Ʈ ����

    private Vector2 oldPos;
    private float timer;

    private void Start()
    {
        if (player == null)
        {
            enabled = false;
            return;
        }

        if (mapIconTransform == null)
        {
            enabled = false;
            return;
        }

        // �� �������� Player ��ũ��Ʈ ���� ����
        mapIconPlayerScript = mapIconTransform.GetComponent<Player>();
        if (mapIconPlayerScript == null)
        {
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

        // �÷��̾ ���߰ų� �浹 ������ �� �� �����ܵ� ����
        if (distance < stuckThreshold || player.IsColliding())
        {
            mapIconPlayerScript.f_Speed = 0;
        }
        else
        {
            mapIconPlayerScript.f_Speed = 2; // �÷��̾ �����̸� �� �����ܵ� ������
        }

        oldPos = player.transform.position; // ���� ��ġ ������Ʈ
    }

}
