using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Suburbs", LoadSceneMode.Single);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void StartNewGame()
    {
        DropObjectController newDrop = new DropObjectController();
        newDrop.readJson("Assets/Resources/Data/PlayerDrop_NEW.json");
        newDrop.writeJson();

        WeaponObjectDataController newWeapons = new WeaponObjectDataController();
        newWeapons.readJson("Assets/Resources/Data/WaeponData_NEW.json");
        newWeapons.writeJson();

        QuestItems newQuestItems = new QuestItems();
        newQuestItems.readJson("Assets/Resources/Data/QuestData_NEW.json");
        newQuestItems.writeJson();

        NPCList newNpcData = new NPCList();
        newNpcData.readJson("Assets/Resources/Data/NPC_NEW.json");
        newNpcData.writeJson();

        PlayerObject newPlayerObject = new PlayerObject();
        newPlayerObject.readJson("Assets/Resources/Data/Player_NEW.json");
        newPlayerObject.writeJson();

        SceneManager.LoadScene("Suburbs", LoadSceneMode.Single);
    }
}
