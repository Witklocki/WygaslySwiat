using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSwitch : MonoBehaviour
{
    public int sceneIndex;

    //public GameObject spawnPoint; 

/*    [SerializeField]
    private InventoryObject inventory;*/


    private void OnTriggerEnter(Collider other)
    {

        string tag = other.gameObject.tag;
        if (tag == "Player")
        {
            StartCoroutine(DelayedSceneLoad(sceneIndex));
        }
    }
    private IEnumerator DelayedSceneLoad(int sceneIndex)
    {
        yield return null; // Wait for one frame

        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
}


