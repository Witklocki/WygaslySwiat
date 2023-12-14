using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TabButton : MonoBehaviour , IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{

    public WeaponUnlockController controller;

    public void OnPointerClick(PointerEventData eventData)
    {
        controller.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        controller.OnTabEntry(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        controller.OnTabExit(this);
    }
}
