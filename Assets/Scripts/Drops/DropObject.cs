using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Drop", menuName = "Inventory System/Drop")]

public class DropObject : ScriptableObject
{
    public int commonElement = 0;
    public int basicElement = 0;
    public int rareElemnt = 0;
    public int elitElemnts = 0;

    public DropObject()
    {
    }

    public DropObject(int commonElement, int basicElement, int rareElemnt, int elitElemnts)
    {
        this.commonElement = commonElement;
        this.basicElement = basicElement;
        this.rareElemnt = rareElemnt;
        this.elitElemnts = elitElemnts;
    }

    public static DropObject operator+ (DropObject a, DropObject b)
    {
        DropObject dropObject = new DropObject();

        dropObject.commonElement = a.commonElement + b.commonElement;
        dropObject.commonElement = a.basicElement + b.basicElement;
        dropObject.commonElement = a.rareElemnt + b.rareElemnt;
        dropObject.commonElement = a.elitElemnts + b.elitElemnts;

        return dropObject;
    }
}
