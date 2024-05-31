using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthStaminaManager : MonoBehaviour
{
    public Image[] healthBlocks; // ü�� ���� �̹��� �迭
    public Image[] staminaBlocks; // ���׹̳� ���� �̹��� �迭

    private int currentHealth;
    private int maxHealth;
    private int currentStamina;
    private int maxStamina;
    private Coroutine staminaCoroutine;
    private Coroutine healthRegenCoroutine;
    private Coroutine staminaRegenCoroutine;

    public float healthRegenDelay = 5f; // ü�� ȸ�� ��� �ð�
    public float staminaRegenDelay = 2f; // ���׹̳� ȸ�� ��� �ð�
    public float regenInterval = 1f; // ȸ�� ����

    void Start()
    {
        maxHealth = healthBlocks.Length;
        currentHealth = maxHealth;

        maxStamina = staminaBlocks.Length;
        currentStamina = maxStamina;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // �׽�Ʈ������ �����̽��ٸ� ������ �� ü�� ����
        {
            TakeDamage(1);
        }

        if (Input.GetKey(KeyCode.LeftShift)) // �޸��� �� ���׹̳� ����
        {
            if (staminaCoroutine == null)
            {
                staminaCoroutine = StartCoroutine(DecreaseStamina());
            }
        }
        else
        {
            if (staminaCoroutine != null)
            {
                StopCoroutine(staminaCoroutine);
                staminaCoroutine = null;
            }

            if (staminaRegenCoroutine == null)
            {
                staminaRegenCoroutine = StartCoroutine(RegenerateStamina());
            }
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;
        UpdateHealthUI();

        if (healthRegenCoroutine != null)
        {
            StopCoroutine(healthRegenCoroutine);
        }
        healthRegenCoroutine = StartCoroutine(RegenerateHealth());
    }

    private void UpdateHealthUI()
    {
        for (int i = 0; i < healthBlocks.Length; i++)
        {
            healthBlocks[i].enabled = i < currentHealth;
        }
    }

    private void UpdateStaminaUI()
    {
        for (int i = 0; i < staminaBlocks.Length; i++)
        {
            staminaBlocks[i].enabled = i < currentStamina;
        }
    }

    private IEnumerator DecreaseStamina()
    {
        while (currentStamina > 0)
        {
            yield return new WaitForSeconds(2f);
            currentStamina--;
            UpdateStaminaUI();

            if (staminaRegenCoroutine != null)
            {
                StopCoroutine(staminaRegenCoroutine);
                staminaRegenCoroutine = null;
            }
        }
    }

    private IEnumerator RegenerateHealth()
    {
        yield return new WaitForSeconds(healthRegenDelay);

        while (currentHealth < maxHealth)
        {
            yield return new WaitForSeconds(regenInterval);
            currentHealth++;
            UpdateHealthUI();
        }
    }

    private IEnumerator RegenerateStamina()
    {
        yield return new WaitForSeconds(staminaRegenDelay);

        while (currentStamina < maxStamina)
        {
            yield return new WaitForSeconds(regenInterval);
            currentStamina++;
            UpdateStaminaUI();
        }
    }
}
