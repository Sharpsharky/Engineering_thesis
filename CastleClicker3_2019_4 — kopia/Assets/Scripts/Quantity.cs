using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Quantity
{


    private List<int> currentValue = new List<int>();

    public Quantity(int initialAmount, int initialQuantityType)
    {
        AddValue(initialAmount, initialQuantityType);
    }
    public Quantity(float initialAmount, int initialQuantityType)
    {
        AddValue(initialAmount, initialQuantityType);
    }
    public List<int> CurrentValue { get => currentValue; set => currentValue = value; }

    public int GetQuantityToDisplay()
    {
        for (int i = (CurrentValue.Count-1); i >= 0; i--)
        {
            if (CurrentValue[i] > 0)
            {
                return i;
            }
        }
        return 0;
    }
    public float GetAmountToDisplay()
    {
        float valueToReturn = 0;

        for (int i = (CurrentValue.Count - 1); i >= 0; i--)
        {
            if (CurrentValue[i] > 0)
            {
                valueToReturn = CurrentValue[i];

                if((i-1) >= 0)
                {
                    if (currentValue[i - 1] > 0)
                    {
                        valueToReturn += Mathf.Round( (CurrentValue[i - 1] * 1.0f / 1000) * 100) / 100.0f; 
                    }


                    if ((i - 2 >= 0))
                    {
                        valueToReturn += Mathf.Round((CurrentValue[i - 2] * 1.0f / 1000) * 100) / 100.0f;
                    }

                }
                return valueToReturn;
            }
        }
        return 0;
    }

    public void AddValue(float amountToAdd, int addedQuantityType)
    {

        if(addedQuantityType > 0)
        {
            amountToAdd *= 1000;
            addedQuantityType--;
        }

        AddCorrectNumberOfItemToList(addedQuantityType);
        CurrentValue[addedQuantityType] += (int)amountToAdd;
        RegulateTheCurrentValue();

    }

    public void AddValue(Quantity quantityToAdd)
    {


        for(int i = 0; i < quantityToAdd.currentValue.Count; i++)
        {
            AddValue(quantityToAdd.currentValue[i], i);
        }

    }


    private void AddCorrectNumberOfItemToList(int quantityType) //3
    {
        if ((quantityType + 1) > CurrentValue.Count)//4 > 0
        {
            int numberOfQuantityTypesToAdd = quantityType + 1 - CurrentValue.Count; //4 - 0

            for (int i = 0; i < numberOfQuantityTypesToAdd; i++)
            {
                CurrentValue.Add(0); //0,1,2,3
            }
        }
    }

    private void RegulateTheCurrentValue()
    {
        for (int i = 0; i < CurrentValue.Count; i++)
        {
            if (CurrentValue[i] >= 1000)
            {
                if (CurrentValue.Count < (i + 2))//4
                {
                    AddCorrectNumberOfItemToList(i + 1);
                }

                CurrentValue[i] -= 1000;
                CurrentValue[i + 1]++;
                RegulateTheCurrentValue();
                break;
            }
        }
    }

    public bool CheckIfThisIsBiggerOrEqualThan(Quantity givenQuantity)
    {
        Quantity cloneOfThisQuantity = CloneThisQuantity(this);
        Quantity cloneOfSubstructingQuantity = CloneThisQuantity(givenQuantity);

        cloneOfThisQuantity.SubstructThisValueByGivenQuantity(cloneOfSubstructingQuantity);

        if (cloneOfThisQuantity.currentValue[0] < 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }


    private Quantity CloneThisQuantity(Quantity quantityToClone)
    {
        Quantity cloneOfThisQuantity = new Quantity(0, 0);

        cloneOfThisQuantity.currentValue[0] = quantityToClone.currentValue[0];
        if (quantityToClone.currentValue.Count > 1)
        {
            for (int i = 1; i < quantityToClone.currentValue.Count; i++)
            {
                cloneOfThisQuantity.currentValue.Add(quantityToClone.currentValue[i]);
            }
        }

        return cloneOfThisQuantity;
    }

    public void SubstructThisValueByGivenQuantity(Quantity substructingQuantity)
    {
        for(int i = 0; i < substructingQuantity.currentValue.Count; i++)
        {
            SubstractValue(substructingQuantity.currentValue[i], i);
        }
    }

    public void SubstractValue(float amount, int quantityType)
    {
        if (amount > 0)
        {

            if (quantityType > 0)
            {
                amount *= 1000;
                quantityType--;
            }

            FillListWithZerosWhereQuantityTypesAreNull(quantityType);

            currentValue[quantityType] -= (int)amount; //Substruct

            AssureThatEveryIndexOfCurrentValueIsntMinus();

            RegulateTheCurrentValue();
        }
        else if(amount < 0) //If its minus minus we are adding value
        {
            AddValue(amount, quantityType);
        }

    }

    private void FillListWithZerosWhereQuantityTypesAreNull(int quantityType)
    {
        if (quantityType + 1 > currentValue.Count) //Fill list with not yet created quantity types
        {
            for (int i = 0; i < currentValue.Count - quantityType + 2; i++)
            {
                currentValue.Add(0);
            }
        }
    }

    private void AssureThatEveryIndexOfCurrentValueIsntMinus()
    {
        for (int i = currentValue.Count - 2; i >= 0; i--)
        {

            if (currentValue[i] < 0) { 
            int added = 0 - currentValue[i + 1];
            Debug.Log("Added to " + i + ": " + added); //400
            currentValue[i + 1] += added;
            currentValue[i] -= 1000 * added;
            }
        }
    }

    public void MultiplyCurrentValueByConstant(int multiplicationConstant)
    {
        for (int i = 0; i < currentValue.Count; i++)
        {
            currentValue[i] *= multiplicationConstant;
        }
        RegulateTheCurrentValue();

    }

}
