using UnityEngine;

public class RecipeSetup : MonoBehaviour
{
    //재료 아이템
    public Item bone; // 뼈 아이템
    public Item leather; // 가죽 아이템
    public Item wood; //목재 아이템
    public Item stone; //돌 아이템
    public Item iron; //철 아이템
    public Item coal; //석탄 아이템
    public Item diamond; // 다이아몬트 아이템

    //제작아이템
    public Item stonepickaxe; // 돌 곡괭이 
    public Item stoneaxe; // 돌 도끼 
    public Item stonesword; // 돌 검 

    public Item Ironpickaxe; // 철 곡괭이
    public Item Ironaxe; // 철 도끼
    public Item Ironsword; // 철 검 

    public Item diamondpickaxe; // 다이아몬드 곡괭이 
    public Item diamondaxe; // 다이아몬드 도끼
    public Item diamondsword; // 다이아몬드 검

    public Item blast; // 용광로
    public Item toach;// 촛불
    public Item craftbox; // 제작대
    public Item Armor1;  //가죽 보호대
    public Item Armor2; //철 보호대 
    public Item Armor3;  //다이아몬드 보호대


    public CombineRecipe[] recipes; // 조합 레시피 배열

    void Start()
    {
        // 새로운 조합 레시피 객체 생성
        CombineRecipe stonepickaxeRecipe = new CombineRecipe
        {
            requiredItems = new Item[] { stone, wood }, // 필요한 아이템 배열 설정
            requiredQuantities = new int[] { 3, 2 }, // 각 아이템의 수량 설정
            resultItem = stonepickaxe // 결과 아이템 설정
        };

        //돌 도끼
        CombineRecipe stoneaxeRecipe = new CombineRecipe
        {
            requiredItems = new Item[] { stone, wood },
            requiredQuantities = new int[] { 3, 2 },
            resultItem = stoneaxe
        };
        //돌 검 
        CombineRecipe stoneswordRecipe = new CombineRecipe
        {
            requiredItems = new Item[] { stone, wood },
            requiredQuantities = new int[] { 2, 1 },
            resultItem = stonesword
        };
        //철 곡괭이
        CombineRecipe IronpickaxeRecipe = new CombineRecipe
        {
            requiredItems = new Item[] { iron, wood },
            requiredQuantities = new int[] { 3, 2 },
            resultItem = Ironaxe
        };
        //철 도끼
        CombineRecipe IronaxeRecipe = new CombineRecipe
        {
            requiredItems = new Item[] { iron, wood },
            requiredQuantities = new int[] { 3, 2 },
            resultItem = Ironaxe
        };
        //철 검
        CombineRecipe IronswordRecipe = new CombineRecipe
        {
            requiredItems = new Item[] { iron, wood },
            requiredQuantities = new int[] { 2, 1 },
            resultItem = Ironsword
        };
        //다이아 곡괭이
        CombineRecipe diamondpickaxeRecipe = new CombineRecipe
        {
            requiredItems = new Item[] { diamond, wood },
            requiredQuantities = new int[] { 3, 2 },
            resultItem = diamondpickaxe
        };
        //다이아 도끼
        CombineRecipe diamondaxeRecipe = new CombineRecipe
        {
            requiredItems = new Item[] { diamond, wood },
            requiredQuantities = new int[] { 3, 2 },
            resultItem = diamondaxe
        };
        //다이아 검
        CombineRecipe diamondswordRecipe = new CombineRecipe
        {
            requiredItems = new Item[] { diamond, wood },
            requiredQuantities = new int[] {2, 1 },
            resultItem = diamondsword
        };
        //용광로
        CombineRecipe blastRecipe = new CombineRecipe
        {
            requiredItems = new Item[] { stone },
            requiredQuantities = new int[] { 8 },
            resultItem = stoneaxe
        };
        //촛불
        CombineRecipe toachRecipe = new CombineRecipe
        {
            requiredItems = new Item[] { coal, wood },
            requiredQuantities = new int[] { 1, 1 },
            resultItem = stoneaxe
        };
        //제작대
        CombineRecipe craftboxRecipe = new CombineRecipe
        {
            requiredItems = new Item[] { wood },
            requiredQuantities = new int[] { 4 },
            resultItem = stoneaxe
        };

        //가죽 보호대
        CombineRecipe Armor1Recipe = new CombineRecipe
        {
            requiredItems = new Item[] { leather },
            requiredQuantities = new int[] {8 },
            resultItem = stoneaxe
        };
        //철 보호대 
        CombineRecipe Armor2Recipe = new CombineRecipe
        {
            requiredItems = new Item[] { iron },
            requiredQuantities = new int[] { 8 },
            resultItem = stoneaxe
        };
        //다이아 보호대
        CombineRecipe Armor3Recipe = new CombineRecipe
        {
            requiredItems = new Item[] { diamond },
            requiredQuantities = new int[] { 8 },
            resultItem = stoneaxe
        };


        // 조합 레시피 배열에 추가
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