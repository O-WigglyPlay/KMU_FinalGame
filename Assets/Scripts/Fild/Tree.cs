using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour
{
    public Animator animator;       // ������ �ִϸ�����
    public float growthDuration = 60f; // ���� �ִϸ��̼��� �ð� (��)
    public GameObject treePrefab;   // ���� ������
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
        // ó���� �� �ڶ� ������ ����
        animator.Play("FullyGrown");
    }

    public void DestroyAndRespawn()
    {
        // ���� �ı�
        Destroy(gameObject);
        // ���� �ð� �� �����
        StartCoroutine(RespawnTree());
    }

    IEnumerator RespawnTree()
    {
        yield return new WaitForSeconds(1f); // ������ �ı��� �� ��� ���
        GameObject newTree = Instantiate(treePrefab, initialPosition, Quaternion.identity);
        newTree.GetComponent<Tree>().StartGrowthAnimation();
    }

    public void StartGrowthAnimation()
    {
        StartCoroutine(GrowthCoroutine());
    }

    IEnumerator GrowthCoroutine()
    {
        animator.Play("Growth");
        yield return new WaitForSeconds(growthDuration);
        animator.Play("FullyGrown");
    }
}
