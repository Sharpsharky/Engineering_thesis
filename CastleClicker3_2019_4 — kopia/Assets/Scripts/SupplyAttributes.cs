using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Supply", menuName = "Supply")]
public class SupplyAttributes : ScriptableObject
{
    public new string name;
    public Sprite icon;
    public int numberInUI;
    public SupplyType supplyType;
}

public enum SupplyType { WOOD, STONE, FOOD, MARBLE, GOLD, SULFUR}