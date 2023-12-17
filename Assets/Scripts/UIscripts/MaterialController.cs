using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    public TextMeshProUGUI common;
    public TextMeshProUGUI basic;
    public TextMeshProUGUI rare;
    public TextMeshProUGUI elite;
    public DropObjectController dropObject;


    // Start is called before the first frame update
    void Start()
    {
        dropObject = new DropObjectController();
        common.text = dropObject.dropObj.commonElement.ToString();
        basic.text = dropObject.dropObj.basicElement.ToString();
        rare.text = dropObject.dropObj.rareElemnt.ToString();
        elite.text = dropObject.dropObj.elitElemnts.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
