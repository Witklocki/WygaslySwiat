using DropModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDropData : MonoBehaviour
{

    DropObject dropObject;

    // Start is called before the first frame update
    void Start()
    {
        transform.localEulerAngles = new Vector3(45, 0, 0);
        dropObject = DropGenerator.generatDrop();
    }

    public DropObject GetDrop()
    {
        return dropObject;
    }

    public void PickedUp()
    {
        Destroy(gameObject);
    }
}
