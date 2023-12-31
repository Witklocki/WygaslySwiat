using DropModel;
using PlayerDataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace DropModel
{
    [Serializable]
    public class DropObject
    {

        public int commonElement;
        public int basicElement;
        public int rareElemnt;
        public int elitElemnts;

        public DropObject()
        {
            commonElement = 0; basicElement = 0; rareElemnt = 0; elitElemnts = 0;
        }

        public static DropObject operator + (DropObject a, DropObject b)
        {
            DropObject dropObject = new DropObject();

            dropObject.commonElement = a.commonElement + b.commonElement;
            dropObject.commonElement = a.basicElement + b.basicElement;
            dropObject.commonElement = a.rareElemnt + b.rareElemnt;
            dropObject.commonElement = a.elitElemnts + b.elitElemnts;

            return dropObject;
        }

        // Overload >= operator
        public static bool operator >=(DropObject left, DropObject right)
        {
            return left.commonElement >= right.commonElement
                && left.basicElement >= right.basicElement
                && left.rareElemnt >= right.rareElemnt
                && left.elitElemnts >= right.elitElemnts;
        }

        // Overload > operator
        public static bool operator >(DropObject left, DropObject right)
        {
            return left.commonElement > right.commonElement
                && left.basicElement > right.basicElement
                && left.rareElemnt > right.rareElemnt
                && left.elitElemnts > right.elitElemnts;
        }

        // Overload <= operator
        public static bool operator <=(DropObject left, DropObject right)
        {
            return left.commonElement <= right.commonElement
                && left.basicElement <= right.basicElement
                && left.rareElemnt <= right.rareElemnt
                && left.elitElemnts <= right.elitElemnts;
        }

        // Overload < operator
        public static bool operator <(DropObject left, DropObject right)
        {
            return left.commonElement < right.commonElement
                && left.basicElement < right.basicElement
                && left.rareElemnt < right.rareElemnt
                && left.elitElemnts < right.elitElemnts;
        }
    }
}


[CreateAssetMenu(fileName = "New Drop", menuName = "Inventory System/Drop")]

public class DropObjectController : ScriptableObject
{
    public DropObject dropObj;
    private static readonly string fileName = "Assets/Resources/Data/PlayerDrop.json";

    public DropObjectController()
    {
        readJson();
    }

    public DropObjectController(int commonElement, int basicElement, int rareElemnt, int elitElemnts)
    {
        dropObj = new DropObject();
        dropObj.commonElement = commonElement;
        dropObj.basicElement = basicElement;
        dropObj.rareElemnt = rareElemnt;
        dropObj.elitElemnts = elitElemnts;
    }

    public void readJson()
    {
        string jsonString = File.ReadAllText(fileName);
        dropObj = JsonUtility.FromJson<DropObject>(jsonString);
    }

    public void readJson(string path)
    {
        string jsonString = File.ReadAllText(path);
        dropObj = JsonUtility.FromJson<DropObject>(jsonString);
    }

    public void writeJson()
    {
        string jsonString = JsonUtility.ToJson(dropObj, true);
        File.WriteAllText(fileName, jsonString);
    }

    public void increment(DropObject dropToAdd)
    {
        dropObj.commonElement += dropToAdd.commonElement;
        dropObj.basicElement += dropToAdd.basicElement;
        dropObj.rareElemnt += dropToAdd.rareElemnt;
        dropObj.elitElemnts += dropToAdd.elitElemnts;
    }

    public void exitIncrement(DropObject dropToAdd)
    {
        increment(dropToAdd);
        writeJson();
    }


}
