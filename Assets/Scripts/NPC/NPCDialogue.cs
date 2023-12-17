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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //print(stringToPrint.ToString());
            ChooseDialog(npcID, npcDialogID);
            setSaveNPC(npcID);

            if (npcAI !=null) { npcAI.isPatrol = false; }
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
        stringToPrint = dataBase.NPCList.data.npc[npcIndex].dialogue[dialogueIndex];
    }
    private void setSaveNPC(int npcIdx)
    {
        npcIndex = npcIdx;
        dataBase.NPCList.data.npc[npcIndex].isSaved = true;
        dataBase.NPCList.writeJson();

    }
}
