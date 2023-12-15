using NPCModel;
using System;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NPCModel
{
    [Serializable]
    public class NPCDataObject
    {
        public int id;
        public string npcName;
        public bool isSaved;
        public bool canFollow;
        public DialogueItem[] dialogue;
    }
    public class DialogueItem
    {
        public string key;
        public string value;
    }

    [Serializable]
    public class NPCData
    {
        public NPCDataObject[] npc;
    }

}



public class NPCList : MonoBehaviour
{
    public NPCData data;
    public int npcIndex;

    private static readonly string fileName = "Assets/Resources/Data/NPC.json";

    public NPCList()
    {
        readJson();
    }

    public void readJson()
    {
        string jsonString = File.ReadAllText(fileName);
        data = JsonUtility.FromJson<NPCData>(jsonString);
    }

    public void writeJson()
    {
        string jsonString = JsonUtility.ToJson(data,true);
        File.WriteAllText(fileName, jsonString);
    }

    public int generateNotSavedNPC()
    {
        int index = Random.Range(0, data.npc.Length);
        if (!data.npc[index].isSaved)
        {
            npcIndex = index;
            return index;
        }
        else
        {
            npcIndex = -1;
            return -1;
        }
    }

    public void npcIsSaved()
    {
        data.npc[npcIndex].isSaved = true;
        writeJson();
    }
}
