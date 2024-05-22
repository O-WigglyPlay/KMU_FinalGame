using UnityEngine;
using System.Collections;

public class MoveObject : MonoBehaviour
{
    public float moveDistance = 5f;  // �̵��� �Ÿ�
    public float moveSpeed = 2f;     // �̵� �ӵ�

    private Vector3 startPosition;
    private bool movingRight = true;

    void Start()
    {
        startPosition = transform.position;
        StartCoroutine(MoveCoroutine());
    }

    IEnumerator MoveCoroutine()
    {
        while (true)
        {
            float targetX = movingRight ? startPosition.x + moveDistance : startPosition.x - moveDistance;
            Vector3 targetPosition = new Vector3(targetX, startPosition.y, startPosition.z);

            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            // ��� ���� �� ���� ��ȯ
            yield return new WaitForSeconds(1f);
            movingRight = !movingRight;
        }
    }
}