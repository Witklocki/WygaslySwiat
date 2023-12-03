using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public MapData map;
    [SerializeField] DB dataBase;

    private void Start()
    {
        if(map.name == "SavePlace")
        {
            GenerateSavedNPC();
        }
        
    }

    private void Update()
    {
        
    }


   public void GenerateSavedNPC()
    {
        for (int i = 0; i < dataBase.NPCList.data.npc.Length; i++)
        {
            if (dataBase.NPCList.data.npc[i].isSaved)
            {
                Debug.Log("Spawn " + dataBase.NPCList.data.npc[i].npcName);
            }
        }
    }
}
