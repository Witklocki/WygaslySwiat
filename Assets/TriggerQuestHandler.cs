using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerQuestHandler : MonoBehaviour
{
    public List<GameObject> qItemsList;
    [SerializeField] DB dataBase;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < dataBase.quests.questitems.Length; i++)
            {
                if (dataBase.quests.questitems[i].isQuest)
                {
                    dataBase.quests.questitems[i].isPickedUp = true;
                    dataBase.quests.questitems[i].isQuest = false;
                    
                }
            }
            dataBase.questItems.writeJson();
        }
        
    }
}
