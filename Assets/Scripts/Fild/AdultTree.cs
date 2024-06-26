using UnityEngine;
using System.Collections;

public class AdultTree : MonoBehaviour
{
    public Animator animator;       // 나무의 애니메이터
    public float growthDuration = 60f; // 성장 애니메이션의 시간 (초)
    public GameObject treePrefab;   // 나무 프리팹
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
        if (animator != null)
        {
            // GameObject 활성화
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }

            // 처음엔 다 자란 나무로 시작
            animator.Play("FullyGrown");
        }
        else
        {
            Debug.LogError("Animator가 할당되지 않았습니다.");
        }
    }

    public void DestroyAndRespawn()
    {
        if (treePrefab != null)
        {
            // 나무 파괴
            Destroy(gameObject);
            // 일정 시간 후 재생성
            StartCoroutine(RespawnTree());
        }
        else
        {
            Debug.LogError("TreePrefab이 할당되지 않았습니다.");
        }
    }

    IEnumerator RespawnTree()
    {
        yield return new WaitForSeconds(1f); // 나무가 파괴된 후 잠시 대기
        GameObject newTree = Instantiate(treePrefab, initialPosition, Quaternion.identity);
        var newTreeScript = newTree.GetComponent<AdultTree>();
        if (newTreeScript != null)
        {
            newTreeScript.StartGrowthAnimation();
        }
        else
        {
            Debug.LogError("생성된 나무에 AdultTree 스크립트가 없습니다.");
        }
    }

    public void StartGrowthAnimation()
    {
        StartCoroutine(GrowthCoroutine());
    }

    IEnumerator GrowthCoroutine()
    {
        if (animator != null)
        {
            // GameObject 활성화
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }

            animator.Play("Growth");
            yield return new WaitForSeconds(growthDuration);
            animator.Play("FullyGrown");
        }
        else
        {
            Debug.LogError("Animator가 할당되지 않았습니다.");
        }
    }
}
