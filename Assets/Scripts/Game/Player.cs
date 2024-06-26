using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int n_Hp;               // 플레이어 체력
    public int n_maxHealth = 100;  // 플레이어 최대 체력
    public float f_Speed;          // 플레이어 스피드
    public int n_Dmg = 1;

    [SerializeField] private Transform attackTransform; // Attack 오브젝트의 Transform
    [SerializeField] private GameObject top_Hitbox; // Top 히트박스 오브젝트
    [SerializeField] private GameObject down_Hitbox; // Down 히트박스 오브젝트
    [SerializeField] private GameObject woodWeapon; // 나무 무기 오브젝트
    [SerializeField] private GameObject mineWeapon; // 광물 무기 오브젝트
    [SerializeField] private GameObject knifeWeapon; // 기본 무기 오브젝트
    [SerializeField] private GameObject hair; // 머리 스프라이트
    [SerializeField] private GameObject body; // 옷 스프라이트

    private Rigidbody2D rb_Player;
    private Animator p_Ani;
    private SpriteRenderer playerSpriteRenderer;
    private Color originalColor; // 원래 색상을 저장하기 위한 변수
    private GameObject currentTree; // 현재 충돌 중인 나무 저장
    private GameObject currentMineral; // 현재 충돌 중인 광물 저장
    private bool isColliding = false; // 충돌 상태를 저장하는 변수
    private Vector2 lastMoveDirection; // 마지막 이동 방향
    private Vector3 initialPosition; // 초기 위치를 저장하는 변수

    public static Player instance;

    private HealthStaminaManager healthStaminaManager;

    private enum WeaponType { None, Mine, Wood, Knife }
    private WeaponType currentWeapon = WeaponType.None;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        rb_Player = GetComponent<Rigidbody2D>();
        p_Ani = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>(); // 플레이어 스프라이트 렌더러 참조
        originalColor = playerSpriteRenderer.color; // 원래 색상 저장
        n_Hp = n_maxHealth;  // 시작할 때 체력 초기화
        n_Dmg = 1;
        initialPosition = transform.position; // 초기 위치 저장

        var attackCollider = attackTransform.GetComponent<BoxCollider2D>();

        // HealthStaminaManager 찾기
        healthStaminaManager = FindObjectOfType<HealthStaminaManager>();
        healthStaminaManager.SetHealth(n_Hp, n_maxHealth); // 초기 체력을 설정

        // 기본 무기 활성화
        knifeWeapon.SetActive(true);
        woodWeapon.SetActive(false);
        mineWeapon.SetActive(false);
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void Update()
    {
        SelectWeapon();
        Attack();
    }

    private void SelectWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = WeaponType.None;
            DeactivateAllWeapons();
            EnableCharacterSprites();
            playerSpriteRenderer.color = originalColor; // 원래 색상으로 복원
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = WeaponType.Mine;
            ActivateWeapon(mineWeapon);
            EnableCharacterSprites();
            playerSpriteRenderer.color = originalColor; // 원래 색상으로 복원
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = WeaponType.Wood;
            ActivateWeapon(woodWeapon);
            EnableCharacterSprites();
            playerSpriteRenderer.color = originalColor; // 원래 색상으로 복원
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentWeapon = WeaponType.Knife;
            ActivateWeapon(knifeWeapon);
            DisableCharacterSprites();
            playerSpriteRenderer.color = new Color(80 / 255.0f, 80 / 255.0f, 80 / 255.0f); // 색상을 80으로 설정
        }
    }

    private void ActivateWeapon(GameObject weapon)
    {
        mineWeapon.SetActive(false);
        woodWeapon.SetActive(false);
        knifeWeapon.SetActive(false);

        weapon.SetActive(true);
    }

    private void DeactivateAllWeapons()
    {
        mineWeapon.SetActive(false);
        woodWeapon.SetActive(false);
        knifeWeapon.SetActive(false);
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float lastMoveX = p_Ani.GetFloat("lastMoveX");
            float lastMoveY = p_Ani.GetFloat("lastMoveY");

            DisableAllHitboxes(); // 모든 히트박스를 비활성화합니다.

            string animationPrefix = "Atk_";
            if (currentWeapon == WeaponType.Knife)
            {
                animationPrefix = "knif_";
            }

            // 좌우 공격
            if (lastMoveX == -1)
            {
                attackTransform.localPosition = new Vector3(-0.033f, attackTransform.localPosition.y, attackTransform.localPosition.z);
                p_Ani.Play(animationPrefix + "left");
                EnableHitbox(attackTransform.gameObject); // 왼쪽 공격 시 attackTransform 히트박스를 활성화합니다.
            }
            else if (lastMoveX == 1)
            {
                attackTransform.localPosition = new Vector3(0.6f, attackTransform.localPosition.y, attackTransform.localPosition.z);
                p_Ani.Play(animationPrefix + "right");
                EnableHitbox(attackTransform.gameObject); // 오른쪽 공격 시 attackTransform 히트박스를 활성화합니다.
            }

            // 상하 공격
            if (lastMoveY == 1)
            {
                EnableHitbox(top_Hitbox);
                p_Ani.Play(animationPrefix + "top");  // 위쪽 공격 애니메이션 실행
            }
            else if (lastMoveY == -1)
            {
                EnableHitbox(down_Hitbox);
                p_Ani.Play(animationPrefix + "bottom");  // 아래쪽 공격 애니메이션 실행
            }

            AttackCollision(); // 공격 충돌 검사를 수행합니다.
        }
    }

    private void EnableHitbox(GameObject hitbox)
    {
        var collider = hitbox.GetComponent<BoxCollider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;
            hitbox.SetActive(true);
        }
    }

    private void DisableHitbox(GameObject hitbox)
    {
        var collider = hitbox.GetComponent<BoxCollider2D>();
        if (collider != null)
        {
            collider.isTrigger = false;
            hitbox.SetActive(false);
        }
    }

    private void DisableAllHitboxes()
    {
        DisableHitbox(top_Hitbox);
        DisableHitbox(down_Hitbox);
        DisableHitbox(attackTransform.gameObject); // attackTransform 히트박스도 비활성화합니다.
    }

    private void PlayerMovement()
    {
        // 플레이어 이동 입력 처리
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // 입력을 기반으로 플레이어 이동 방향 설정
        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        // 입력에 따라 플레이어의 속도 설정
        rb_Player.velocity = movement * f_Speed;

        if (movement != Vector2.zero)
        {
            lastMoveDirection = movement;
        }

        p_Ani.SetFloat("moveX", horizontalInput);
        p_Ani.SetFloat("moveY", verticalInput);
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 ||
            Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            p_Ani.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            p_Ani.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
        }
    }

    public Vector2 GetLastMoveDirection()
    {
        return lastMoveDirection.normalized; // 방향을 정규화하여 반환
    }

    public void Die()
    {
        Time.timeScale = 0f; // 게임 일시정지
        UIManager.instance.ShowDeathPanel(); // Death 패널 활성화
    }

    public void Respawn()
    {
        transform.position = initialPosition; // 초기 위치로 이동
        n_Hp = n_maxHealth; // 체력 초기화
        healthStaminaManager.SetHealth(n_Hp, n_maxHealth); // UI 업데이트
        Time.timeScale = 1f; // 게임 재개
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isColliding = true;
        if (collision.gameObject.CompareTag("Tree"))
        {
            currentTree = collision.gameObject;  // 나무 오브젝트를 현재 충돌한 나무로 설정
        }
        else if (collision.gameObject.CompareTag("Mineral"))
        {
            currentMineral = collision.gameObject;  // 광물 오브젝트를 현재 충돌한 광물로 설정
        }

        if (collision.gameObject.name == "Square")
        {
            TakeDamage(10);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tree") || collision.gameObject.CompareTag("Mineral"))
        {
            isColliding = true;  // 충돌 상태를 지속적으로 갱신
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isColliding = false;
        if (collision.gameObject.CompareTag("Tree"))
        {
            if (currentTree == collision.gameObject)
            {
                currentTree = null;  // 나무와의 충돌이 끝났다면 참조 제거
            }
        }
        else if (collision.gameObject.CompareTag("Mineral"))
        {
            if (currentMineral == collision.gameObject)
            {
                currentMineral = null;  // 광물과의 충돌이 끝났다면 참조 제거
            }
        }
    }

    public bool IsColliding()
    {
        return isColliding; // 충돌 상태가 유지되는지 확인
    }

    public void TakeDamage(int amount)
    {
        n_Hp -= amount;
        if (n_Hp < 0) n_Hp = 0;

        if (healthStaminaManager != null)
        {
            healthStaminaManager.SetHealth(n_Hp, n_maxHealth); // 체력 갱신
        }

        if (n_Hp <= 0)
        {
            Die();
        }
    }

    public void AttackCollision()
    {

        if (currentTree != null)
        {
            TreeMng treeScript = currentTree.GetComponent<TreeMng>();
            if (treeScript != null)
            {
                if (currentWeapon == WeaponType.Wood)
                {
                    treeScript.Tree_Hp -= 3;  // 나무 무기 사용 시 3 데미지
                }
                else
                {
                    treeScript.Tree_Hp -= n_Dmg;  // 나머지 무기는 기본 데미지
                }
            }
        }
        else if (currentMineral != null)
        {
            MChange mineralScript = currentMineral.GetComponent<MChange>();
            if (mineralScript != null)
            {
                if (currentWeapon == WeaponType.Mine)
                {
                    mineralScript.TakeDamage(3);  // 광물 무기 사용 시 3 데미지
                }
                else
                {
                    mineralScript.TakeDamage(n_Dmg);  // 나머지 무기는 기본 데미지
                }
                if (mineralScript.Mineral_Hp <= 0)
                {
                    currentMineral = null; // 파괴 후 currentMineral 참조 제거
                }
            }
        }
    }

    private void DisableCharacterSprites()
    {
        hair.SetActive(false);
        body.SetActive(false); // 옷 스프라이트 비활성화
    }

    private void EnableCharacterSprites()
    {
        hair.SetActive(true);
        body.SetActive(true); // 옷 스프라이트 활성화
    }
}
