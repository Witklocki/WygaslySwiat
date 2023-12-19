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
        npcAI.isPatrol = true;
        StopTyping();
        ClearTextField();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name == "Tao")
        {
            if (other.CompareTag("Player"))
            {

                npcIndex = dataBase.NPCList.data.npc[0].id;
                dialogueIndex = 3;
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
        if (other.CompareTag("Player") && gameObject.name != "Tao")
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
        dialoguePanel.SetActive(false);

    }
    public void StopTyping()
    {
        if (isTyping)
        {
            StopCoroutine("Typing");
            isTyping = false;
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
}
