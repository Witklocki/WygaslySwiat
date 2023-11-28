using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadWeapon : MonoBehaviour
{
    public FixedJoystick attackJoystick;

    public static bool baseballExist = false;
    public static bool knifeExist = false;
    public static bool crowbarExist = false;
    public static bool remingtonExist = false;
    public static bool rewolwerExist = false;
    public static bool MP5Exist = false;
    public static bool glockExist = false;

    public static bool baseballIsEquipped = false;
    public static bool knifeIsEquipped = false;
    public static bool crowbarIsEquipped = false;
    public static bool remingtonIsEquipped = false;
    public static bool glockIsEquipped = false;
    public static bool MP5IsEquipped = false;
    public static bool rewolwerIsEquipped = false;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Suburbs" || scene.name == "SavePlace")
        {
            FindAndAssignJoystick<FixedJoystick>("AttackJoystick");
        }
        if (baseballIsEquipped) { baseballExist = true; } else { baseballExist = false; }
        if (knifeIsEquipped) { knifeExist = true; } else { knifeExist = false; }
        if (remingtonIsEquipped) { remingtonExist = true; } else { remingtonExist = false; }
        if (crowbarIsEquipped) { crowbarExist = true; } else { crowbarExist = false; }
        if (rewolwerIsEquipped) { rewolwerExist = true; } else { rewolwerExist = false; }
        if (MP5IsEquipped) { MP5Exist = true; } else { MP5Exist = false; }
        if (glockIsEquipped) { glockExist = true; } else { glockExist = false; }

    }

    private void FindAndAssignJoystick<T>(string joystickName) where T : FixedJoystick
    {
        Canvas canvas = FindObjectOfType<Canvas>();

        if (canvas != null)
        {
            T[] attackJoysticks = canvas.GetComponentsInChildren<T>();

            if (attackJoysticks.Length > 0)
            {
                attackJoystick = attackJoysticks[1];
            }
            else
            {
                Debug.LogWarning($"{joystickName} not found in the Canvas.");
            }
        }
        else
        {
            Debug.LogWarning("Canvas not found in the scene.");
        }
    }


}