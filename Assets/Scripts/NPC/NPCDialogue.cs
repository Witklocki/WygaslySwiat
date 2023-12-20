using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class NPCDialogue : MonoBehaviour
{
    
    public GameObject dialoguePanel;
    public GameObject continueButton;
    public TextMeshProUGUI dialogueText;
    public float speed = 0.05f;
    [SerializeField] public DB dataBase;
    private int dialogueIndex;
    private int npcIndex;
    private NPCAIScript npcAI;
    private string stringToPrint;
    private bool isTyping = false;

    public int npcID = -1;
    public int npcDialogID;

    void Start()
    {
        dialogueText.text = "";
        npcAI = GetComponent<NPCAIScript>();
    }


    private void OnTriggerExit(Collider other)
    {
        if (gameObject.GetComponent<NPCAIScript>() != null)
        {
            npcAI.isPatrol = true;
        }

        StopTyping();
        ClearTextField();
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (gameObject.name == "Tao")
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Tao");
                npcIndex = dataBase.NPCList.data.npc[0].id;
                QuestDialogeGet();
                if (!dialoguePanel.activeInHierarchy)
                {
                    if (gameObject.GetComponent<NPCAIScript>() != null)
                    {
                        npcAI.isPatrol = false;
                    }
                    ChooseDialog(npcIndex, dialogueIndex);

                    dialoguePanel.SetActive(true);
                    StartCoroutine(Typing());

                }
                else
                {
                    ClearTextField();
                }
            
            }

        }
        if (gameObject.name == "Neal")
        {
            if (other.CompareTag("Player"))
            {
                ChooseDialog(1, 2);

                Debug.Log("Neal");
                if (!dialoguePanel.activeInHierarchy)
                {
                    if (gameObject.GetComponent<NPCAIScript>() != null)
                    {
                        npcAI.isPatrol = false;
                    }


                    dialoguePanel.SetActive(true);
                    StartCoroutine(Typing());

                }
                else
                {
                    ClearTextField();
                }

            }
        }
        if (gameObject.name == "Dimitri")
        {
            if (other.CompareTag("Player"))
            {
                ChooseDialog(2, 2);

                Debug.Log("Dimitri");
                if (!dialoguePanel.activeInHierarchy)
                {
                    if (gameObject.GetComponent<NPCAIScript>() != null)
                    {
                        npcAI.isPatrol = false;
                    }


                    dialoguePanel.SetActive(true);
                    StartCoroutine(Typing());
                }
                else
                {
                    ClearTextField();
                }

            }
        }
        if (gameObject.name == "Cole")
        {
            if (other.CompareTag("Player"))
            {
                ChooseDialog(3, 2);

                Debug.Log("Cole");
                if (!dialoguePanel.activeInHierarchy)
                {
                    if (gameObject.GetComponent<NPCAIScript>() != null)
                    {
                        npcAI.isPatrol = false;
                    }


                    dialoguePanel.SetActive(true);
                    StartCoroutine(Typing());
                }
                else
                {
                    ClearTextField();
                }

            }
        }
        if (other.CompareTag("Player") && gameObject.name != "Tao" && gameObject.name != "Neal" && gameObject.name != "Dimitri" && gameObject.name != "Cole")
        {
            if(enabled)
            {

                ChooseDialog(npcID, npcDialogID);
                setSaveNPC(npcID);
                enabled = false;
                if (npcAI != null) { npcAI.isPatrol = false; }
                if (!dialoguePanel.activeInHierarchy)
                {
                    dialoguePanel.SetActive(true);
                    StartCoroutine(Typing());
                }
                else
                {
                    ClearTextField();
                }
            }
        }
    }
    public void ClearTextField()
    {
        dialogueText.text = "";
        stringToPrint = "";

        dialoguePanel.SetActive(false);

    }
    public void StopTyping()
    {
        if (isTyping)
        {
            StopCoroutine("Typing");
            isTyping = false;
            dialogueText.text = "";
            stringToPrint = "";

        }
    }
    public void NextLine()
    {
        continueButton.SetActive(false);
        ClearTextField();
        isTyping = false;
    }
    IEnumerator Typing()
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in stringToPrint.ToCharArray())
        {
            if (isTyping)
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(speed);
            }
            else
            {
                dialogueText.text = "";
                stringToPrint = "";

                break;
            }
        }

        continueButton.SetActive(true);
        isTyping = false;
    }
    private void ChooseDialog(int npcIdx , int dialogueIdx)
    {
        dialogueText.text = "";
        stringToPrint = "";

        npcIndex = npcIdx;
        dialogueIndex = dialogueIdx;
        stringToPrint = dataBase.NPCList.data.npc[npcIndex].dialogue[dialogueIndex]; //error

    }
    private void setSaveNPC(int npcIdx)
    {
        npcIndex = npcIdx;
        dataBase.NPCList.data.npc[npcIndex].isSaved = true;
        dataBase.NPCList.writeJson();

    }

    public void QuestDialogeGet()
    {
        if (dataBase.NPCList.data.npc[npcIndex].npcName == "Tao")
        {
            for(int i = 0; i < dataBase.quests.questitems.Length; i++)
            {
                if (dataBase.quests.questitems[i].isQuest)
                {
                    dialogueIndex = i + 3;
                    return;
                }
                else if (!dataBase.quests.questitems[i].isQuest && !dataBase.quests.questitems[i].isGiven && !dataBase.quests.questitems[i].isPickedUp)
                {
                    dialogueIndex = i + 3;
                    dataBase.quests.questitems[i].isQuest = true;
                    dataBase.questItems.writeJson();
                    return;
                }
                else if (dataBase.quests.questitems[i].isPickedUp && !dataBase.quests.questitems[i].isGiven)
                {
                    if (i + 4 < 7)
                    {
                        dialogueIndex = i + 4;
                    }
                    else
                    {
                        dialogueIndex = i + 3;
                    }
                    dataBase.quests.questitems[i].isGiven = true;
                    dataBase.questItems.writeJson();
                    return;
                }
            }
        }
    }
}
