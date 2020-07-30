
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingInhancements
{
    public BuildingInhencementType inhencementType;
    public int coefficientIncreasingPrice;
    public List<Price> supplyPrices = new List<Price>();
}

[System.Serializable]
public struct Price
{
    public float amount;
    public int quantityType;
    public int startingLevel;
    public SupplyType supplyType;
}

public enum BuildingInhencementType { PER_CLICK, IN_TIME, STORAGE_SPACE, NEW_BUILDING}
