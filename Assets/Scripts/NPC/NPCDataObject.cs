using NPCModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
        public string[] dialogue;
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

    public List<int> npcIndex;

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

    public void readJson(string path)
    {
        string jsonString = File.ReadAllText(path);
        data = JsonUtility.FromJson<NPCData>(jsonString);
    }

    public void writeJson()
    {
        string jsonString = JsonUtility.ToJson(data,true);
        File.WriteAllText(fileName, jsonString);
    }

    public List<int> generateNotSavedNPC()
    {
        npcIndex = new List<int>();
        for (int i = 0; i < data.npc.Length; i++)
        {
            if (!data.npc[i].isSaved)
            {
                npcIndex.Add(i);
            }
        }
        return npcIndex;

    }

    public void npcIsSaved()
    {
        for (int i = 0; i < data.npc.Length; i++)
        {
            data.npc[i].isSaved = true;
        }
        writeJson();
    }
}
