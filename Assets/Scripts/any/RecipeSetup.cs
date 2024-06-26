using UnityEngine;

public class RecipeSetup : MonoBehaviour
{
    public Item bone; // �� ������
    public Item leather; // ���� ������
    public Item pickaxe; // ��� ������

    public CombineRecipe[] recipes; // ���� ������ �迭

    void Start()
    {
        // ���ο� ���� ������ ��ü ����
        CombineRecipe pickaxeRecipe = new CombineRecipe
        {
            requiredItems = new Item[] { bone, leather }, // �ʿ��� ������ �迭 ����
            requiredQuantities = new int[] { 3, 2 }, // �� �������� ���� ����
            resultItem = pickaxe // ��� ������ ����
        };

        // ���� ������ �迭�� �߰�
        recipes = new CombineRecipe[] { pickaxeRecipe };
    }
}