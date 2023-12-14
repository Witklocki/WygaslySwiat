using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUnlockController : MonoBehaviour
{
    [SerializeField] List<TabButton> tabButtons;
    [SerializeField] List<GameObject> objectSwap;

    TabButton selectedButton;

    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }
        tabButtons.Add(button);
    }

    public void OnTabEntry(TabButton button)
    {

    }

    public void OnTabExit(TabButton button)
    {

    }

    public void OnTabSelected(TabButton button)
    {
        selectedButton = button;
        int index = button.transform.GetSiblingIndex();
        print(index);
        for (int i = 0; i < objectSwap.Count; i++)
        {
            if (i.Equals(index))
            {
                objectSwap[i].SetActive(true);
            }
            else
            {
                objectSwap[i].SetActive(false);
            }
        }
    }

    public void ResetButtons()
    {
        foreach (var button in tabButtons)
        {
            //do sth;
        }
    }
}
