using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HungerManager : MonoBehaviour
{
    public Image[] hungerBlocks; // ����� ��� �̹��� �迭
    private int currentHunger; // ���� ����� ����
    private int maxHunger; // �ִ� ����� ����
    private Coroutine hungerCoroutine; // ����� ���� �ڷ�ƾ ���� ����

    public float hungerDecreaseInterval = 60f; // ����� �⺻ ���� ���� (��)
    public float hungerDecreaseDuringStamina = 40f; // ���׹̳� ��� �� ����� ���� ���� (��)

    void Start()
    {
        maxHunger = hungerBlocks.Length; // �ִ� ����� ���¸� �̹��� �迭 ���̷� ����
        currentHunger = maxHunger; // ���� �� ���� ������� �ִ� ��������� ����
        hungerCoroutine = StartCoroutine(DecreaseHunger(hungerDecreaseInterval)); // ����� ���� �ڷ�ƾ ����
        UpdateHungerUI(); // ���� �� UI ������Ʈ
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)) // �޸��� �� ����� ���� �ӵ� ����
        {
            if (hungerCoroutine != null)
            {
                StopCoroutine(hungerCoroutine); // ���� ����� ���� �ڷ�ƾ ����
                hungerCoroutine = StartCoroutine(DecreaseHunger(hungerDecreaseDuringStamina)); // �� ���� ���� �ӵ��� �ڷ�ƾ �����
            }
        }
        else
        {
            if (hungerCoroutine != null)
            {
                StopCoroutine(hungerCoroutine); // ���� ����� ���� �ڷ�ƾ ����
                hungerCoroutine = StartCoroutine(DecreaseHunger(hungerDecreaseInterval)); // �⺻ ���� �ӵ��� �ڷ�ƾ �����
            }
        }
    }

    private void UpdateHungerUI()
    {
        Debug.Log("Updating Hunger UI: Current Hunger = " + currentHunger); // ����� �α� ���
        for (int i = 0; i < hungerBlocks.Length; i++)
        {
            hungerBlocks[i].enabled = i < currentHunger; // ���� ����� ���¿� ���� �̹��� Ȱ��ȭ/��Ȱ��ȭ
            Debug.Log("Hunger Block " + i + " is " + (hungerBlocks[i].enabled ? "enabled" : "disabled")); // �� ����� Ȱ��ȭ ���� ���
        }
    }

    private IEnumerator DecreaseHunger(float interval)
    {
        while (currentHunger > 0) // ������� 0���� ū ����
        {
            yield return new WaitForSeconds(interval); // ������ �ð� ���� ���
            currentHunger--; // ����� 1 ����
            Debug.Log("Decreasing Hunger: Current Hunger = " + currentHunger); // ����� �α� ���
            UpdateHungerUI(); // UI ������Ʈ
        }
    }
}
