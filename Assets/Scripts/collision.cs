using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        switch (tag)
        {
            case "Exit":
                print("Emtry Exit");
                break;
            case "lab":
                print("Emtry lab");
                break;
            case "storage":
                print("Emtry storage");
                break;
            case "armory":
                print("Emtry armory");
                break;
            case "savedPeople":
                print("Emtry savedPeople");
                break;
        }

    }


    private void OnTriggerStay(Collider other)
    {

    }
    private void OnTriggerExit(Collider other)
    {
 
    }
}
