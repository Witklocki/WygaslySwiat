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
    public string[] dialogue;
    public float speed = 0.05f;
    public bool endDialogue = false;
    [SerializeField] DB dataBase;
    private int index = 0;
    private int NPCindex = 0;
    private NPCAIScript npcAI;


    void Start()
    {
        dialogueText.text = "";
        npcAI = GetComponent<NPCAIScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(dataBase.NPCList.data.npc[0].dialogue[1].ToString());

            npcAI.isPatrol = false;
            endDialogue = false;
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
        index = 0;
        dialoguePanel.SetActive(false);

    }
    public void NextLine()
    {
        continueButton.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            endDialogue = true;
            ClearTextField();
        }

    }
    IEnumerator Typing()
    {
        foreach(char letter in dataBase.NPCList.data.npc[NPCindex].dialogue[index+1].ToString().ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(speed);
        }
        continueButton.SetActive(true);

    }

}
