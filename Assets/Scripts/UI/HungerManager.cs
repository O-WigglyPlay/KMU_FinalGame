using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HungerManager : MonoBehaviour
{
    public Image[] hungerBlocks; // 배고픔 블록 이미지 배열
    private int currentHunger; // 현재 배고픔 상태
    private int maxHunger; // 최대 배고픔 상태
    private Coroutine hungerCoroutine; // 배고픔 감소 코루틴 참조 변수

    public float hungerDecreaseInterval = 60f; // 배고픔 기본 감소 간격 (초)
    public float hungerDecreaseDuringStamina = 40f; // 스테미나 사용 시 배고픔 감소 간격 (초)

    void Start()
    {
        maxHunger = hungerBlocks.Length; // 최대 배고픔 상태를 이미지 배열 길이로 설정
        currentHunger = maxHunger; // 시작 시 현재 배고픔을 최대 배고픔으로 설정
        hungerCoroutine = StartCoroutine(DecreaseHunger(hungerDecreaseInterval)); // 배고픔 감소 코루틴 시작
        UpdateHungerUI(); // 시작 시 UI 업데이트
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)) // 달리기 시 배고픔 감소 속도 증가
        {
            if (hungerCoroutine != null)
            {
                StopCoroutine(hungerCoroutine); // 기존 배고픔 감소 코루틴 정지
                hungerCoroutine = StartCoroutine(DecreaseHunger(hungerDecreaseDuringStamina)); // 더 빠른 감소 속도로 코루틴 재시작
            }
        }
        else
        {
            if (hungerCoroutine != null)
            {
                StopCoroutine(hungerCoroutine); // 기존 배고픔 감소 코루틴 정지
                hungerCoroutine = StartCoroutine(DecreaseHunger(hungerDecreaseInterval)); // 기본 감소 속도로 코루틴 재시작
            }
        }
    }

    private void UpdateHungerUI()
    {
        Debug.Log("Updating Hunger UI: Current Hunger = " + currentHunger); // 디버그 로그 출력
        for (int i = 0; i < hungerBlocks.Length; i++)
        {
            hungerBlocks[i].enabled = i < currentHunger; // 현재 배고픔 상태에 따라 이미지 활성화/비활성화
            Debug.Log("Hunger Block " + i + " is " + (hungerBlocks[i].enabled ? "enabled" : "disabled")); // 각 블록의 활성화 상태 출력
        }
    }

    private IEnumerator DecreaseHunger(float interval)
    {
        while (currentHunger > 0) // 배고픔이 0보다 큰 동안
        {
            yield return new WaitForSeconds(interval); // 지정된 시간 간격 대기
            currentHunger--; // 배고픔 1 감소
            Debug.Log("Decreasing Hunger: Current Hunger = " + currentHunger); // 디버그 로그 출력
            UpdateHungerUI(); // UI 업데이트
        }
    }
}
