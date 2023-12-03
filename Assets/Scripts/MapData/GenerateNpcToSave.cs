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

    public BoxCollider boxCollider;

    public int npcIndex;


    private void Start()
    {
        housesList = GameObject.FindGameObjectsWithTag("House");
        if (dataBase.NPCList.generateNotSavedNPC() != -1)
        {
            int index = Random.Range(0, housesList.Length - 1);
            SpriteRenderer spriteRenderer = housesList[index].GetComponentInChildren<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = houseWithLight;
                BoxCollider box;
                box = housesList[index].AddComponent<BoxCollider>();
                box.isTrigger = true;
                box.center = new Vector3(-0.000175941f, 0.2292919f, 0.07555886f);
                box.size = new Vector3(0.6f, 0.5f, 0.6f);
            }
        }
        else
        {
            Debug.Log("Nie ma");
        }
    }


    private void Update()
    {

    }
}
