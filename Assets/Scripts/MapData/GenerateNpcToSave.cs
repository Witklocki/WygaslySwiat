using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GenerateNpcToSave : MonoBehaviour
{
    public Sprite normalHouse;
    public Sprite houseWithLight;

    public GameObject[] housesList;
    [SerializeField] DB dataBase;
    private NPCDialogue npcDialogue;
    public BoxCollider boxCollider;

    public List<int> npcIndex;

    private List<int> modifiedHouseIndices = new List<int>();
    private void Start()
    {
        Debug.Log(dataBase.NPCList.data.npc.Length);
        Canvas canvas = FindObjectOfType<Canvas>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        housesList = GameObject.FindGameObjectsWithTag("House");
        for (int i = 0; i < dataBase.NPCList.data.npc.Length; i++)
        {
            if (!dataBase.NPCList.data.npc[i].isSaved) 
            {
                npcIndex = dataBase.NPCList.generateNotSavedNPC();

                int index;
                do
                { 
                    index = Random.Range(0, housesList.Length - 1); 
                } while (modifiedHouseIndices.Contains(index));

                SpriteRenderer spriteRenderer = housesList[index].GetComponentInChildren<SpriteRenderer>();

                if (spriteRenderer != null)
                {

                    spriteRenderer.sprite = houseWithLight;
                    modifiedHouseIndices.Add(index);

                    npcDialogue = housesList[index].GetComponent<NPCDialogue>();
                    npcDialogue.enabled = true;
                    Transform dialoguePanelObject = canvas.transform.Find("DialoguePanel");
                    Transform dbObject = playerObject.transform.Find("DBController");
                    Transform continueButtonObject = dialoguePanelObject.transform.Find("ContinueButton");
                    Transform dialogueTextObject = dialoguePanelObject.transform.Find("DialogueText");

                    npcDialogue.dialoguePanel = dialoguePanelObject.gameObject;
                    npcDialogue.continueButton = continueButtonObject.gameObject;
                    npcDialogue.dialogueText = dialogueTextObject.GetComponent<TMPro.TextMeshProUGUI>();
                    npcDialogue.dataBase = dbObject.GetComponent<DB>();

                    //BoxCollider box;
                    //box = housesList[index].AddComponent<BoxCollider>();
                    //box.isTrigger = true;
                    //box.center = new Vector3(-0.000175941f, 0.2292919f, 0.07555886f);
                    //box.size = new Vector3(0.6f, 0.5f, 0.6f);
                    npcDialogue.npcID = dataBase.NPCList.data.npc[i].id;
                    npcDialogue.npcDialogID = 1;
                }

            } 
        }
    }

}
