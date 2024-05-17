using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitCtrl : MonoBehaviour
{
    public float speed = 5f; // 투사체의 속도
    public int damage = 10; // 플레이어에게 줄 데미지
    private Vector2 direction;

    void Start()
    {
        // 플레이어 게임 오브젝트 찾기
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // 플레이어의 위치와 투사체의 위치를 사용하여 방향 설정
            Vector2 dir = player.transform.position - transform.position;
            SetDirection(dir);
        }
        else
        {
            Debug.LogError("플레이어를 찾을 수 없습니다.");
        }
    }
    public void SetDirection(Vector2 dir)
    {
        direction = dir;
        // 방향에 따라 투사체 이동 설정
        GetComponent<Rigidbody2D>().velocity = direction * speed; // speed 변수는 정의되어 있어야 합니다.
         
        // 투사체가 플레이어를 바라보도록 회전 설정
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Update()
    {

    }

    // 충돌 처리 (필요에 따라 수정 가능)
    void OnCollisionEnter2D(Collision2D collision)  
    {
        // 충돌한 오브젝트가 플레이어인지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            // 충돌 시 투사체 파괴
            Destroy(gameObject);

            // 플레이어의 PlayerHealth 스크립트를 가져와서 데미지 처리
            Player playerHealth = collision.gameObject.GetComponent<Player>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
