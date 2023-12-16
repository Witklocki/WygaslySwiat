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
                        BoxCollider box;
                        box = housesList[index].AddComponent<BoxCollider>();
                        box.isTrigger = true;
                        box.center = new Vector3(-0.000175941f, 0.2292919f, 0.07555886f);
                        box.size = new Vector3(0.6f, 0.5f, 0.6f);
                    }

            } 
        }
    }

    private void FindCanvasForObject(GameObject houseObject)
    {
        
    }
}
