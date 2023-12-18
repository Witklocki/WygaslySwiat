using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using QuestItemModel;

namespace QuestItemModel
{
    [Serializable]
    public class QuestItemObject
    {
        public int id;
        public string name;
        public bool isQuest;
        public bool isPickedUp;
        public bool isGiven;
    }
    [Serializable]
    public class QuestItemList
    {
        public QuestItemObject[] questitems;
        
    }
}



public class QuestItems : ScriptableObject
{
    public QuestItemList qObj;
    private static readonly string fileName = "Assets/Resources/Data/QuestData.json";

    public QuestItems()
    {
        readJson();
    }
    public void readJson()
    {
        string jsonString = File.ReadAllText(fileName);
        qObj = JsonUtility.FromJson<QuestItemList>(jsonString);
    }

    public void writeJson()
    {
        string jsonString = JsonUtility.ToJson(qObj, true);
        File.WriteAllText(fileName, jsonString);
    }
}
