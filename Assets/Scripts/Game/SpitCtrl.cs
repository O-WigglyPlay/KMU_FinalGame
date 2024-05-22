using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitCtrl : MonoBehaviour
{
    public float speed = 5f; // ����ü�� �ӵ�
    public int damage = 10; // �÷��̾�� �� ������
    private Vector2 direction;

    void Start()
    {
        // �÷��̾� ���� ������Ʈ ã��
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player != null)
        {
            // �÷��̾��� ��ġ�� ����ü�� ��ġ�� ����Ͽ� ���� ����
            Vector2 dir = player.transform.position - transform.position;
            SetDirection(dir);
        }
        else
        {
            Debug.LogError("�÷��̾ ã�� �� �����ϴ�.");
        }
    }
    public void SetDirection(Vector2 dir)
    {
        direction = dir;
        // ���⿡ ���� ����ü �̵� ����
        GetComponent<Rigidbody2D>().velocity = direction * speed; // speed ������ ���ǵǾ� �־�� �մϴ�.
         
        // ����ü�� �÷��̾ �ٶ󺸵��� ȸ�� ����
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Update()
    {

    }
}
