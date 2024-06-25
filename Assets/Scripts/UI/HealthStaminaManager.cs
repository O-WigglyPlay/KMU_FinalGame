using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthStaminaManager : MonoBehaviour
{
    public Image[] healthBlocks; // 체력 블록 이미지 배열
    public Image[] staminaBlocks; // 스태미나 블록 이미지 배열

    private int currentHealth;
    private int maxHealth;
    private int currentStamina;
    private int maxStamina;
    private Coroutine staminaCoroutine;
    private Coroutine healthRegenCoroutine;
    private Coroutine staminaRegenCoroutine;

    public float healthRegenDelay = 5f; // 체력 회복 대기 시간
    public float staminaRegenDelay = 2f; // 스태미나 회복 대기 시간
    public float regenInterval = 1f; // 회복 간격

    void Start()
    {
        maxHealth = healthBlocks.Length;
        currentHealth = maxHealth;

        maxStamina = staminaBlocks.Length;
        currentStamina = maxStamina;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 테스트용으로 스페이스바를 누르면 체력을 감소
        {
            TakeDamage(1);
        }

        if (Input.GetKey(KeyCode.LeftShift)) // 달리기 시 스태미나 감소
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

    public void SetHealth(int health, int maxHealth)
    {
        currentHealth = Mathf.Clamp(health, 0, maxHealth);
        this.maxHealth = maxHealth;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        int activeBlocks = Mathf.RoundToInt(healthBlocks.Length * healthPercentage);

        for (int i = 0; i < healthBlocks.Length; i++)
        {
            healthBlocks[i].enabled = i < activeBlocks;
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

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void IncreaseStamina(int amount)
    {
        currentStamina += amount;
        if (currentStamina > maxStamina) currentStamina = maxStamina;
        UpdateStaminaUI();
    }
}
