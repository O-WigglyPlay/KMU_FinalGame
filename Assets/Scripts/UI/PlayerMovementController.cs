using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerMovementController : MonoBehaviour
{
    public TextMeshProUGUI displayText; // TMP UI Text를 참조
    public string[] messages; // 출력할 메시지 배열
    public float displayDuration = 2.0f; // 텍스트 표시 시간
    public float moveSpeed = 2.0f; // 이동 속도
    public Transform triggerPoint; // 트리거 포인트 참조

    private int currentMessageIndex = 0;
    private bool isMoving = false;

    private void Start()
    {
        if (displayText != null)
        {
            displayText.gameObject.SetActive(false); // 게임 시작 시 텍스트를 비활성화
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
            // y축 방향으로 일정한 속도로 이동
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
            displayText.gameObject.SetActive(true); // 텍스트 활성화
            displayText.text = messages[currentMessageIndex]; // 현재 메시지를 텍스트로 표시
            currentMessageIndex = (currentMessageIndex + 1) % messages.Length; // 다음 메시지 인덱스 설정

            yield return new WaitForSeconds(displayDuration); // 일정 시간 대기
        }
    }
}
