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

    public int npcID = -1;
    public int npcDialogID;

    void Start()
    {
        dialogueText.text = "";
        npcAI = GetComponent<NPCAIScript>();
        dataBase = GameObject.FindWithTag("Player").GetComponentInChildren<DB>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name == "Tao")
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Taoishere");
                ChooseDialog(0 ,2);

            }

        }
        if (other.CompareTag("Player") && gameObject.name != "Tao")
        {
            //print(stringToPrint.ToString());
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
    public void NextLine()
    {
        continueButton.SetActive(false);
        dialogueText.text = "";
        ClearTextField();
    }
    IEnumerator Typing()
    {
        dialogueText.text = "";
        foreach(char letter in stringToPrint.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(speed);
        }
        continueButton.SetActive(true);
    }
    private void ChooseDialog(int npcIdx , int dialogueIdx)
    {
        npcIndex = npcIdx;
        dialogueIndex = dialogueIdx;
        Debug.Log("sth0");
        stringToPrint = dataBase.NPCList.data.npc[npcIndex].dialogue[dialogueIndex]; //error 
    }
    private void setSaveNPC(int npcIdx)
    {
        npcIndex = npcIdx;
        dataBase.NPCList.data.npc[npcIndex].isSaved = true;
        dataBase.NPCList.writeJson();

    }
}
