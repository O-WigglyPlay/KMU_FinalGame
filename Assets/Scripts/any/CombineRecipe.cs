using System;
using UnityEngine;

[System.Serializable]
public class CombineRecipe
{
    public Item[] requiredItems; // 필요한 아이템 배열
    public int[] requiredQuantities; // 각 아이템의 필요한 수량 배열
    public Item resultItem; // 조합 결과로 얻을 수 있는 아이템
}