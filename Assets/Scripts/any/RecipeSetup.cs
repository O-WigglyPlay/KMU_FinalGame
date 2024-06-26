using UnityEngine;

public class RecipeSetup : MonoBehaviour
{
    //��� ������
    public Item bone; // �� ������
    public Item leather; // ���� ������
    public Item wood; //���� ������
    public Item stone; //�� ������
    public Item iron; //ö ������
    public Item coal; //��ź ������
    public Item diamond; // ���̾Ƹ�Ʈ ������

    //���۾�����
    public Item stonepickaxe; // �� ��� 
    public Item stoneaxe; // �� ���� 
    public Item stonesword; // �� �� 

    public Item Ironpickaxe; // ö ���
    public Item Ironaxe; // ö ����
    public Item Ironsword; // ö �� 

    public Item diamondpickaxe; // ���̾Ƹ�� ��� 
    public Item diamondaxe; // ���̾Ƹ�� ����
    public Item diamondsword; // ���̾Ƹ�� ��

    public Item blast; // �뱤��
    public Item toach;// �к�
    public Item craftbox; // ���۴�
    public Item Armor1;  //���� ��ȣ��
    public Item Armor2; //ö ��ȣ�� 
    public Item Armor3;  //���̾Ƹ�� ��ȣ��


    public CombineRecipe[] recipes; // ���� ������ �迭

    void Start()
    {
        // ���ο� ���� ������ ��ü ����
        CombineRecipe stonepickaxeRecipe = new CombineRecipe
        {
            requiredItems = new Item[] { stone, wood }, // �ʿ��� ������ �迭 ����
            requiredQuantities = new int[] { 3, 2 }, // �� �������� ���� ����
            resultItem = stonepickaxe // ��� ������ ����
        };

        //�� ����
        CombineRecipe stoneaxeRecipe = new CombineRecipe
        {
            requiredItems = new Item[] { stone, wood },
            requiredQuantities = new int[] { 3, 2 },
            resultItem = stoneaxe
        };
        //�� �� 
        CombineRecipe stoneswordRecipe = new CombineRecipe
        {
            requiredItems = new Item[] { stone, wood },
            requiredQuantities = new int[] { 2, 1 },
            resultItem = stonesword
        };
        //ö ���
        CombineRecipe IronpickaxeRecipe = new CombineRecipe
        {
            requiredItems = new Item[] { iron, wood },
            requiredQuantities = new int[] { 3, 2 },
            resultItem = Ironaxe
        };
        //ö ����
        CombineRecipe IronaxeRecipe = new CombineRecipe
        {
            requiredItems = new Item[] { iron, wood },
            requiredQuantities = new int[] { 3, 2 },
            resultItem = Ironaxe
        };
        //ö ��
        CombineRecipe IronswordRecipe = new CombineRecipe
        {
            requiredItems = new Item[] { iron, wood },
            requiredQuantities = new int[] { 2, 1 },
            resultItem = Ironsword
        };
        //���̾� ���
        CombineRecipe diamondpickaxeRecipe = new CombineRecipe
        {
            requiredItems = new Item[] { diamond, wood },
            requiredQuantities = new int[] { 3, 2 },
            resultItem = diamondpickaxe
        };
        //���̾� ����
        CombineRecipe diamondaxeRecipe = new CombineRecipe
        {
            requiredItems = new Item[] { diamond, wood },
            requiredQuantities = new int[] { 3, 2 },
            resultItem = diamondaxe
        };
        //���̾� ��
        CombineRecipe diamondswordRecipe = new CombineRecipe
        {
            requiredItems = new Item[] { diamond, wood },
            requiredQuantities = new int[] {2, 1 },
            resultItem = diamondsword
        };
        //�뱤��
        CombineRecipe blastRecipe = new CombineRecipe
        {
            requiredItems = new Item[] { stone },
            requiredQuantities = new int[] { 8 },
            resultItem = stoneaxe
        };
        //�к�
        CombineRecipe toachRecipe = new CombineRecipe
        {
            requiredItems = new Item[] { coal, wood },
            requiredQuantities = new int[] { 1, 1 },
            resultItem = stoneaxe
        };
        //���۴�
        CombineRecipe craftboxRecipe = new CombineRecipe
        {
            requiredItems = new Item[] { wood },
            requiredQuantities = new int[] { 4 },
            resultItem = stoneaxe
        };

        //���� ��ȣ��
        CombineRecipe Armor1Recipe = new CombineRecipe
        {
            requiredItems = new Item[] { leather },
            requiredQuantities = new int[] {8 },
            resultItem = stoneaxe
        };
        //ö ��ȣ�� 
        CombineRecipe Armor2Recipe = new CombineRecipe
        {
            requiredItems = new Item[] { iron },
            requiredQuantities = new int[] { 8 },
            resultItem = stoneaxe
        };
        //���̾� ��ȣ��
        CombineRecipe Armor3Recipe = new CombineRecipe
        {
            requiredItems = new Item[] { diamond },
            requiredQuantities = new int[] { 8 },
            resultItem = stoneaxe
        };


        // ���� ������ �迭�� �߰�
        recipes = new CombineRecipe[] { stonepickaxeRecipe };
        recipes = new CombineRecipe[] { stoneaxeRecipe };
        recipes = new CombineRecipe[] { stoneswordRecipe };
        recipes = new CombineRecipe[] { IronpickaxeRecipe };
        recipes = new CombineRecipe[] { IronaxeRecipe };
        recipes = new CombineRecipe[] { IronswordRecipe };
        recipes = new CombineRecipe[] { diamondpickaxeRecipe };
        recipes = new CombineRecipe[] { diamondaxeRecipe };
        recipes = new CombineRecipe[] { diamondswordRecipe };
        recipes = new CombineRecipe[] { blastRecipe };
        recipes = new CombineRecipe[] { craftboxRecipe };
        recipes = new CombineRecipe[] { toachRecipe };
        recipes = new CombineRecipe[] { Armor1Recipe };
        recipes = new CombineRecipe[] { Armor2Recipe };
        recipes = new CombineRecipe[] { Armor3Recipe };

    }
}