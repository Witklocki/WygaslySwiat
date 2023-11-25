using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSwitch : MonoBehaviour
{
    //Variable used for storing the value of the scene index
    public int sceneIndex;
    
    ///Method responsible for triggering the change of the scene
    private void OnTriggerEnter(Collider other)
    {
        //Variable responsible for storing the tag of the Game Object
        string tag = other.gameObject.tag;
        //Debug TODELETE
        Debug.Log("Scene Change Trigger Entered");
        //Check if the Game Object is a Player
        if (tag == "Player")
        {
            Debug.Log("Switching to scene: " + sceneIndex);
            //Loading the appropriate scene
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }
    }
}
