using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB : MonoBehaviour
{
    public NPCList NPCList;
    public bool npcSavingInProgres = false;

    private void Awake()
    {
        NPCList = new NPCList();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
