using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class NealScript : MonoBehaviour
{
    
    public GameObject dialoguePanel;
    public GameObject continueButton;
    public TextMeshProUGUI dialogueText;
    public float speed = 0.05f;
    [SerializeField] DB dataBase;
    private int dialogueIndex;
    private int npcIndex;
    private NPCAIScript npcAI;
    private string stringToPrint;
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
            ChooseDialog(0, 2);
            npcAI.isPatrol = false;
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
}
