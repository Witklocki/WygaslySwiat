using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GenerateQuestObject : MonoBehaviour
{
    [SerializeField] public GameObject[] questObjectsToShow;
    [SerializeField] DB dataBase;
   
    void Start()
    {
        for (int i = 0; i < dataBase.quests.questitems.Length; i++)
        {
            if (dataBase.quests.questitems[i].isQuest && !dataBase.quests.questitems[i].isPickedUp)
            {
                questObjectsToShow[i].SetActive(true);
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < dataBase.quests.questitems.Length; i++)
        {
            if (!dataBase.quests.questitems[i].isQuest && dataBase.quests.questitems[i].isPickedUp)
            {
                questObjectsToShow[i].SetActive(false);
            }
        }
    }
}
