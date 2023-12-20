using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinScript : MonoBehaviour
{
    // Start is called before the first frame update

    QuestItems quest;
    public GameObject endScreen;

    public void quit()
    {
        Application.Quit();
    }
}
