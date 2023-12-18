using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateQuestObject : MonoBehaviour
{

    public List<GameObject> qItemsList;
    public QuestItems questItems;
    void Start()
    {
        questItems = new QuestItems();
        GenerateItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateItems()
    {
        for (int i=0; i < questItems.qObj.questitems.Length; i++)
        {
            if (questItems.qObj.questitems[i].isQuest)
            {
                qItemsList[i].SetActive(true);
            }
            else
            {
                qItemsList[i].SetActive(false);
            }
        }

    }
}
