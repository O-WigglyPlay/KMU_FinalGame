using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerMovementController : MonoBehaviour
{
    public TextMeshProUGUI displayText; // TMP UI Text�� ����
    public string[] messages; // ����� �޽��� �迭
    public float displayDuration = 2.0f; // �ؽ�Ʈ ǥ�� �ð�
    public float moveSpeed = 2.0f; // �̵� �ӵ�
    public Transform triggerPoint; // Ʈ���� ����Ʈ ����

    private int currentMessageIndex = 0;
    private bool isMoving = false;

    private void Start()
    {
        if (displayText != null)
        {
            displayText.gameObject.SetActive(false); // ���� ���� �� �ؽ�Ʈ�� ��Ȱ��ȭ
            Debug.Log($"[{gameObject.name}] Start: Display text is set to inactive.");
        }
        else
        {
            Debug.LogError($"[{gameObject.name}] Start: Display text is not assigned.");
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            // y�� �������� ������ �ӵ��� �̵�
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.transform == triggerPoint)
        {
            Debug.Log($"[{gameObject.name}] OnTriggerEnter: Player reached the trigger point.");
            StartMovement();
        }
    }

    private void StartMovement()
    {
        isMoving = true;
        StartCoroutine(DisplayMessages());
    }

    private IEnumerator DisplayMessages()
    {
        while (isMoving)
        {
            displayText.gameObject.SetActive(true); // �ؽ�Ʈ Ȱ��ȭ
            displayText.text = messages[currentMessageIndex]; // ���� �޽����� �ؽ�Ʈ�� ǥ��
            currentMessageIndex = (currentMessageIndex + 1) % messages.Length; // ���� �޽��� �ε��� ����

            yield return new WaitForSeconds(displayDuration); // ���� �ð� ���
        }
    }
}
