using System;
using UnityEngine;

[System.Serializable]
public class CombineRecipe
{
    public Item[] requiredItems; // �ʿ��� ������ �迭
    public int[] requiredQuantities; // �� �������� �ʿ��� ���� �迭
    public Item resultItem; // ���� ����� ���� �� �ִ� ������
}