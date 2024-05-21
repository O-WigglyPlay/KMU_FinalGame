using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animals : MonoBehaviour
{
    public int health = 100;
    public float moveSpeed; // 동물의 이동 속도

    public GameObject meat; //동물을 죽였을 때 나오는 고기
    public GameObject bone; //동물을 죽였을 때 나오는 뼈
    public GameObject leather; //동물을 죽였을 때 나오는 가죽
    public GameObject teeth; //동물을 죽였을 때 나오는 이빨


    public float meatDropChance = 0.25f; // 고기 아이템 드랍 확률
    public float boneDropChance = 0.25f; // 뼈 아이템 드랍 확률
    public float LeatherDropChance = 0.25f; // 가죽 아이템 드랍 확률
    public float teethDropChance = 0.25f; // 가죽 아이템 드랍 확률

    private Animator animator; // 동물의 애니메이터
    private Transform playerTransform; // 플레이어의 Transform 컴포넌트

    Rigidbody2D rigid;
    SpriteRenderer spriter;

    bool isLive = true;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();  //충돌 
        rigid.freezeRotation = true; // 회전을 고정
        spriter = GetComponent<SpriteRenderer>();   //Sprite 불러오기
        animator = GetComponent<Animator>(); // 애니메이터 컴포넌트 가져오기
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // 충돌한 대상이 플레이어인지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            // 동물의 체력을 감소시킴
            health -= 10; // 예를 들어 10의 데미지를 줄 수 있음

            if (health <= 0)
            {
                Die(); // 동물이 사망할 경우 처리하는 함수 호출
            }
        }
    }

    // 동물이 사망할 때 호출되는 함수
    void Die()
    {
        // 여기에 동물이 죽었을 때의 처리를 추가할 수 있음
        if(isLive)
        {
            isLive  = false;

            if(Random.value <= meatDropChance)
            {
                //고기 아이템 생성
                Instantiate(meat, transform.position, Quaternion.identity);
            }
            if (Random.value <= meatDropChance)
            {
                //뼈 아이템 생성
                Instantiate(bone, transform.position, Quaternion.identity);
            }
            if (Random.value <= meatDropChance)
            {
                //가죽 아이템 생성
                Instantiate(leather, transform.position, Quaternion.identity);
            }
            if (Random.value <= meatDropChance)
            {
                //이빨 아이템 생성
                Instantiate(teeth, transform.position, Quaternion.identity);
            }
            Destroy(gameObject); // 동물 오브젝트 삭제
        }
        
    }
}