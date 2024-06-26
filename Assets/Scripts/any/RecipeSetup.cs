using UnityEngine;

public class RecipeSetup : MonoBehaviour
{
    public Item bone; // 뼈 아이템
    public Item leather; // 가죽 아이템
    public Item pickaxe; // 곡괭이 아이템

    public CombineRecipe[] recipes; // 조합 레시피 배열

    void Start()
    {
        // 새로운 조합 레시피 객체 생성
        CombineRecipe pickaxeRecipe = new CombineRecipe
        {
            requiredItems = new Item[] { bone, leather }, // 필요한 아이템 배열 설정
            requiredQuantities = new int[] { 3, 2 }, // 각 아이템의 수량 설정
            resultItem = pickaxe // 결과 아이템 설정
        };

        // 조합 레시피 배열에 추가
        recipes = new CombineRecipe[] { pickaxeRecipe };
    }
}