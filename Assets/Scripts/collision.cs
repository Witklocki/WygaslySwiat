using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Exit")
        {
            print("Emtry");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Exit")
        {
            print("Inside");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Exit")
        {
            print("Exit");
        }
    }
}
